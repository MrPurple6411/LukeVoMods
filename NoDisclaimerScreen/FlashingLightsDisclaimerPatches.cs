namespace NoDisclaimerScreen;

using HarmonyLib;

[HarmonyPatch(typeof(FlashingLightsDisclaimer))]
public class FlashingLightsDisclaimerPatches
{
    [HarmonyPatch(nameof(FlashingLightsDisclaimer.GetShowTime)), HarmonyPrefix]
    public static bool GetShowTime(ref float __result)
    {
        __result = 1E10f; // Has been showing for a few years
        return false;
    }
}
