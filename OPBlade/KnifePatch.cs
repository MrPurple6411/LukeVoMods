namespace OPBlade;

using HarmonyLib;

[HarmonyPatch(typeof(Knife), nameof(Knife.OnToolUseAnim))]
public class KnifePatch
{

    public static void Prefix(Knife __instance)
    {
        if (__instance is HeatBlade)
        {
            __instance.damage = Config.HeatBladeDamage;
            __instance.attackDist = Config.HeatBladeRange;
            __instance.spikeyTrapDamage = Config.HeatBladeSpikyTrapDamage;
            return;
        }

        __instance.damage = Config.KnifeDamage;
        __instance.attackDist = Config.KnifeRange;
        __instance.spikeyTrapDamage = Config.KnifeSpikyTrapDamage;
    }
}
