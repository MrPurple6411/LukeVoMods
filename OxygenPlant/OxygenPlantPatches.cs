namespace OxyPlant;
#if BELOWZERO

using HarmonyLib;

[HarmonyPatch(typeof(OxygenPlant))]
public static class OxygenPlantPatches
{
    [HarmonyPatch(nameof(GetProgress)), HarmonyPrefix]
    public static void GetProgress(OxygenPlant __instance)
    {
        SetValues(__instance);
    }

    [HarmonyPatch(nameof(OnHandClick)), HarmonyPrefix]
    public static void OnHandClick(OxygenPlant __instance)
    {
        SetValues(__instance);
    }

    static void SetValues(OxygenPlant __instance)
    {
        __instance.duration = Config.Duration;
        __instance.capacity = Config.Capacity;
    }
}

#endif