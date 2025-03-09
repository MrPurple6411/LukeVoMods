namespace FasterGrowth;

using HarmonyLib;

[HarmonyPatch(typeof(GrowingPlant), nameof(GrowingPlant.GetGrowthDuration))]
public class GrowingPlantPatcher
{
    public static void Postfix(ref float __result)
    {
        __result *= Config.DurationMultiplier;
    }
}
