namespace SeaglideOvercharge.Patches;

using HarmonyLib;
using SeaglideOvercharge;
using System.Collections.Generic;
using UnityEngine;

[HarmonyPatch(typeof(Seaglide))]
public static class SeaglidePatches
{
    static bool AnnouncementShown = false;
    static readonly Dictionary<Seaglide, SeaglideOverchargeInfo> info = new();

    public static SeaglideOverchargeInfo recentOvercharge;

    public static SeaglideOverchargeInfo GetOrCreateInfo(this Seaglide seaglide)
    {
        if (!info.TryGetValue(seaglide, out var result))
        {
            result = info[seaglide] = new SeaglideOverchargeInfo(seaglide);
        }

        return result;
    }

    [HarmonyPatch(nameof(Update)), HarmonyPostfix]
    public static void Update(Seaglide __instance)
    {
        var info = __instance.GetOrCreateInfo();
        if (info.overchargeTime > 0)
        {
            info.overchargeTime = Mathf.Max(0, info.overchargeTime - Time.deltaTime);

            if (info.overchargeTime <= 0)
            {
                ResetMotorMode();
            }
        }

        if (__instance.activeState)
        {
            if (!AnnouncementShown)
            {
                AnnouncementShown = true;
                ErrorMessage.AddError($"Press {Config.OverchargeKey} to activate Overcharge.");
            }

            if (Input.GetKeyDown(Config.OverchargeKey))
            {
                TryActivatingOvercharge(info);
            }
        }
    }

    static void TryActivatingOvercharge(SeaglideOverchargeInfo info)
    {
        // Only activate if none is activated
        if (info.overchargeTime > 0)
        {
            ErrorMessage.AddError("Seaglide is still in Overcharged mode");
            return;
        }

        // Check battery
        var energyConsumption = Config.OverchargeEnergyConsumption;
        var eMixin = info.seaglide.energyMixin;
        if (eMixin.charge <= energyConsumption)
        {
            ErrorMessage.AddError("Not enough battery charge");
            return;
        }

        var player = Player.main;
        if (player == null)
        {
            Plugin.Logger.LogDebug("Player null");
            return;
        }

        // Boost
        info.overchargeTime = Config.OverchargeDuration;
        info.overchargeMul = Config.OverchargeBoost;
        eMixin.ConsumeEnergy(energyConsumption);

        recentOvercharge = info;

        // Set new Motor Mode to player to update speed
        ResetMotorMode(player);

        ErrorMessage.AddError($"Seaglide Overcharged for {(int)info.overchargeTime} seconds.");
    }

    static void ResetMotorMode(Player player = null)
    {
        if (player == null) { player = Player.main; }
        if (player == null) { return; }

        player.SetMotorMode(Player.MotorMode.Dive);
        player.SetMotorMode(Player.MotorMode.Seaglide);
    }
}

public class SeaglideOverchargeInfo
{
    public Seaglide seaglide;

    public float overchargeTime = 0f;
    public float overchargeMul = 0f;

    public SeaglideOverchargeInfo(Seaglide seaglide)
    {
        this.seaglide = seaglide;
    }
}
