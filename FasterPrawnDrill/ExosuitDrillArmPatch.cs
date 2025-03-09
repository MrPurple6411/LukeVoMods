namespace FasterPrawnDrill;

using HarmonyLib;
using System;
using UnityEngine;

[HarmonyPatch(typeof(ExosuitDrillArm), nameof(ExosuitDrillArm.OnHit))]
public class ExosuitDrillArmPatch
{
    [HarmonyPrefix]
    public static void Prefix(ExosuitDrillArm __instance)
    {
        try
        {
            Exosuit exosuit = __instance.exosuit;

            if (exosuit.CanPilot() && exosuit.GetPilotingMode())
            {
                Vector3 zero = Vector3.zero;
                GameObject gameObject = null;
                UWE.Utils.TraceFPSTargetPosition(exosuit.gameObject, 5f, ref gameObject, ref zero, out Vector3 vector, true);

                if (gameObject != null && __instance.drilling)
                {
                    Drillable drillable = gameObject.FindAncestor<Drillable>();

                    if (drillable)
                    {
                        var maxHealth = Config.MaxDrillHealth;
                        var healths = drillable.health;

                        for (int i = 0; i < healths.Length; i++)
                        {
                            if (healths[i] > maxHealth)
                            {
                                healths[i] = maxHealth;
                            }
                        }
                        return;
                    }

                    float damage = Config.AddOtherDamage;
                    if (Mathf.Approximately(damage, 0f))
                    {
                        return;
                    }
                    LiveMixin liveMixin = gameObject.FindAncestor<LiveMixin>();
                    if (liveMixin)
                    {
                        liveMixin.IsAlive();
                        liveMixin.TakeDamage(damage, zero, DamageType.Drill, null);
                    }
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Plugin.Logger.LogError($"Error in {nameof(ExosuitDrillArmPatch)}.{nameof(Prefix)}: {ex.Message}");
            Plugin.Logger.LogError(ex.StackTrace);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                Plugin.Logger.LogError($"InnerException: {ex.Message}");
                Plugin.Logger.LogError(ex.StackTrace);
            }
        }
    }
}
