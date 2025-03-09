#if BELOWZERO
namespace ExtendedMineralDetector;

using HarmonyLib;

[HarmonyPatch(typeof(MetalDetector), nameof(MetalDetector.Start))]
public class MineralDetectorPatch
{
    [HarmonyPrefix]
    public static void Prefix(MetalDetector __instance)
    {
        __instance.powerConsumption = Config.PowerConsumption;
        __instance.scanDistance = Config.ScanDistance;
    }
}
#endif