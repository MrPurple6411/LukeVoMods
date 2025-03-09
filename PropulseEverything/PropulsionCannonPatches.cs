namespace PropulseEverything;

using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

[HarmonyPatch(typeof(PropulsionCannon))]
public class PropulsionCannonPatches
{
    [HarmonyPatch(nameof(Filter)), HarmonyPrefix]
    public static bool Filter(InventoryItem item, PropulsionCannon __instance, ref bool __result)
    {
        var baseInstance = (MonoBehaviour)__instance;
        __result = !(item == null || item.item == null || baseInstance.transform.IsChildOf(item.item.GetComponent<Transform>()));

        return false;
    }

    [HarmonyPatch(nameof(PropulsionCannon.TraceForGrabTarget)), HarmonyPrefix]
    public static void TraceForGrabTargetPre(PropulsionCannon __instance)
    {
        __instance.maxMass = float.MaxValue;
        __instance.maxAABBVolume = float.MaxValue;
        __instance.pickupDistance = Config.PickupDistance;
        __instance.attractionForce = Config.AttractionForce;
        __instance.shootForce = Config.ShootForce;
        __instance.massScalingFactor = Config.MassScalingFactor;
    }

    [HarmonyPatch(nameof(TraceForGrabTarget)), HarmonyPostfix]
    public static void TraceForGrabTarget(PropulsionCannon __instance, ref GameObject __result)
    {
        if (__result != null)
        {
            return;
        }

        Vector3 position = MainCamera.camera.transform.position;
        int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        int num = UWE.Utils.SpherecastIntoSharedBuffer(position, 1.2f, MainCamera.camera.transform.forward, __instance.pickupDistance, layerMask);
        GameObject result = null;
        float num2 = float.PositiveInfinity;
        var checkedObjects = new HashSet<GameObject>();

        for (int i = 0; i < num; i++)
        {
            RaycastHit raycastHit = UWE.Utils.sharedHitBuffer[i];

            var layer = raycastHit.collider.gameObject.layer;
            if (raycastHit.collider.isTrigger && (layer <= 8 || layer >= 19))
            {
                continue;
            }

            GameObject entityRoot = UWE.Utils.GetEntityRoot(raycastHit.collider.gameObject);
            if (!(entityRoot != null) || checkedObjects.Contains(entityRoot))
            {
                continue;
            }

            float sqrMagnitude = (raycastHit.point - position).sqrMagnitude;
            if (sqrMagnitude < num2 && __instance.ValidateNewObject(entityRoot, raycastHit.point))
            {
                result = entityRoot;
                num2 = sqrMagnitude;

            }
            checkedObjects.Add(entityRoot);
        }

        __result = result;
    }

    [HarmonyPatch(nameof(ValidateObject)), HarmonyPrefix]
    public static bool ValidateObject(GameObject go, PropulsionCannon __instance, ref bool __result)
    {
        if (!go.activeSelf || !go.activeInHierarchy)
        {
            __result = false;
            Debug.Log("object is inactive");
            return false;
        }

        __result = go.GetComponent<Rigidbody>() != null;
        return false;
    }

    [HarmonyPatch(nameof(ValidateNewObject)), HarmonyPrefix]
    public static bool ValidateNewObject(GameObject go, PropulsionCannon __instance, ref bool __result)
    {
        bool result = false;
        ValidateObject(go, __instance, ref result);

        __result = result;
        return false;
    }

    [HarmonyPatch(nameof(ReleaseGrabbedObject)), HarmonyPrefix]
    public static bool ReleaseGrabbedObject(PropulsionCannon __instance)
    {
        if (__instance.grabbedObject != null)
        {
            PropulseCannonAmmoHandler component = __instance.grabbedObject.GetComponent<PropulseCannonAmmoHandler>();

            if (component != null)
            {
                component.UndoChanges();
                UnityEngine.Object.Destroy(component);
            }

            __instance.grabbedObject = null;
        }

        return false;
    }

    [HarmonyPatch(nameof(Update)), HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Update(IEnumerable<CodeInstruction> instructions)
    {
        var matcher = new CodeMatcher(instructions);

        matcher.MatchForward(false,
            new CodeMatch(OpCodes.Ldc_R4, .7f))
            .Set(OpCodes.Call, typeof(PropulsionCannonPatches).GetMethod(nameof(GetDynamicEnergyRate)));

        return matcher.InstructionEnumeration();
    }

    public static float GetDynamicEnergyRate()
    {
        return Config.EnergyRate * .7f;
    }
}
