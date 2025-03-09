#if BELOWZERO
namespace MineralDetectorBlip;

using HarmonyLib;

[HarmonyPatch(typeof(MetalDetector), nameof(MetalDetector.LateUpdate))]
public class MetalDetectorPatch
{
    [HarmonyPostfix]
    public static void Postfix(MetalDetector __instance, ResourceTrackerDatabase.ResourceInfo ___closestResourceNode, MetalDetector.ScreenState ___screenState)
    {
        if (!__instance.isDrawn || ___closestResourceNode == null || ___screenState != MetalDetector.ScreenState.Tracking)
        {
            return;
        }

        var vector = ___closestResourceNode.position - MainCamera.camera.transform.position;
        var len = vector.magnitude;
        var y = vector.y;

        __instance.screenTooltipText.text = $"{len:0}m ({y:0}m)";
    }
}
#endif