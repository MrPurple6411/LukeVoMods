namespace HoverBikeOnWater.Patches;
#if BELOWZERO

using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

[HarmonyPatch(typeof(Hoverbike))]
public static class HoverbikePatches
{
    [HarmonyPatch(nameof(Awake)), HarmonyPrefix]
    public static void Awake(Hoverbike __instance)
    {
        UpdateHoverbike(__instance);
    }

    [HarmonyPatch(nameof(Hoverbike.HoverEngines)), HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> HoverEnginesTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var matcher = new CodeMatcher(instructions)
            .MatchForward(false,
                new CodeMatch(OpCodes.Ldc_I4_1),
                new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(Hoverbike), "overWater")));

        matcher.SetInstruction(new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(HoverbikePatches), nameof(HoverOnWater))));

        return matcher.InstructionEnumeration();
    }

    public static bool HoverOnWater()
    {
        return !Config.HoverOnWater;
    }

    internal static void UpdateHoverbike(Hoverbike hoverbike)
    {
        hoverbike.topSpeed = Config.TopSpeed;
        hoverbike.enginePowerConsumption = Config.EnergyConsumption;
        hoverbike.toggleLights.energyPerSecond = Config.LightEnergyConsumption;

        var rb = hoverbike.rb;
        rb.drag = Config.Drag;
        rb.angularDrag = Config.AngularDrag;

        var head = hoverbike.headManager;
        head.rollSpring = Config.RollSpring;
        head.yawSpring = Config.YawSpring;
        head.pitchSpring = Config.PitchSpring;
        head.rollAngleDeadzone = Config.RollAngleDeadzone;

        head.minViewConeAperture = Config.MinViewConeAperture;
        head.maxViewConeAperture = Config.MaxViewConeAperture;

        hoverbike.liveMixin.data.maxHealth = Config.MaxHealth;
    }

}

#endif