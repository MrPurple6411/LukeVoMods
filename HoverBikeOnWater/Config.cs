namespace HoverBikeOnWater;
#if BELOWZERO

using HoverBikeOnWater.Patches;
using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;
using UnityEngine;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

    public static bool Loaded => Instance != null;

    [JsonProperty("HoverOnWater")]
    [Toggle("Hover On Water", Tooltip = "Enable or disable hovering on water", Order = 1)]
    public bool hoverOnWater = true;

    [JsonProperty("TopSpeed")]
    [Slider("Top Speed", 1f, 20f, DefaultValue = 11f, Step = 0.1f, Order = 2), OnChange(nameof(UpdateAllHoverbikes))]
    public float topSpeed = 11f;

    [JsonProperty("Drag")]
    [Slider("Drag", 0.1f, 1f, DefaultValue = 0.8f, Step = 0.01f, Order = 3), OnChange(nameof(UpdateAllHoverbikes))]
    public float drag = .8f;

    [JsonProperty("AngularDrag")]
    [Slider("Angular Drag", 0.1f, 1f, DefaultValue = 1f, Step = 0.01f, Order = 4), OnChange(nameof(UpdateAllHoverbikes))]
    public float angularDrag = 1f;

    [JsonProperty("PitchSpring")]
    [Slider("Pitch Spring", 0.1f, 10f, DefaultValue = 5f, Step = 0.1f, Order = 5), OnChange(nameof(UpdateAllHoverbikes))]
    public float pitchSpring = 5f;

    [JsonProperty("YawSpring")]
    [Slider("Yaw Spring", 0.1f, 720f, DefaultValue = 360f, Step = 0.1f, Order = 6), OnChange(nameof(UpdateAllHoverbikes))]
    public float yawSpring = 360f;

    [JsonProperty("MinViewConeAperture")]
    [Slider("Min View Cone Aperture", 0f, 20f, DefaultValue = 0f, Step = 0.1f, Order = 7), OnChange(nameof(UpdateAllHoverbikes))]
    public float minViewConeAperture = 0f;

    [JsonProperty("MaxViewConeAperture")]
    [Slider("Max View Cone Aperture", 0f, 20f, DefaultValue = 10f, Step = 0.1f, Order = 8), OnChange(nameof(UpdateAllHoverbikes))]
    public float maxViewConeAperture = 10f;

    [JsonProperty("RollSpring")]
    [Slider("Roll Spring", 0.1f, 10f, DefaultValue = 2.5f, Step = 0.1f, Order = 9), OnChange(nameof(UpdateAllHoverbikes))]
    public float rollSpring = 2.5f;

    [JsonProperty("RollAngleDeadzone")]
    [Slider("Roll Angle Deadzone", 0f, 90f, DefaultValue = 45f, Step = 0.1f, Order = 10), OnChange(nameof(UpdateAllHoverbikes))]
    public float rollAngleDeadzone = 45f;

    [JsonProperty("EnergyConsumption")]
    [Slider("Energy Consumption", 0f, 1f, DefaultValue = 0.06666f, Step = 0.0001f, Order = 11), OnChange(nameof(UpdateAllHoverbikes))]
    public float energyConsumption = 0.06666f;

    [JsonProperty("LightEnergyConsumption")]
    [Slider("Light Energy Consumption", 0f, 1f, DefaultValue = 0f, Step = 0.0001f, Order = 12), OnChange(nameof(UpdateAllHoverbikes))]
    public float lightEnergyConsumption = 0f;

    [JsonProperty("SummonKey")]
    [Keybind("Summon Key", Order = 13)]
    public KeyCode summonKey = KeyCode.F;

    [JsonProperty("SummonEnergyPerMeter")]
    [Slider("Summon Energy Per Meter", 0f, 1f, DefaultValue = 0.1f, Step = 0.0001f, Order = 14)]
    public float summonEnergyPerMeter = .1f;

    [JsonProperty("MaxHealth")]
    [Slider("Max Health", 1f, 1000f, DefaultValue = 200f, Step = 1f, Order = 15), OnChange(nameof(UpdateAllHoverbikes))]
    public float maxHealth = 200f;

    public static bool HoverOnWater => Instance.hoverOnWater;
    public static float TopSpeed => Instance.topSpeed;
    public static float Drag => Instance.drag;
    public static float AngularDrag => Instance.angularDrag;
    public static float PitchSpring => Instance.pitchSpring;
    public static float YawSpring => Instance.yawSpring;
    public static float MinViewConeAperture => Instance.minViewConeAperture;
    public static float MaxViewConeAperture => Instance.maxViewConeAperture;
    public static float RollSpring => Instance.rollSpring;
    public static float RollAngleDeadzone => Instance.rollAngleDeadzone;
    public static float EnergyConsumption => Instance.energyConsumption;
    public static float LightEnergyConsumption => Instance.lightEnergyConsumption;
    public static KeyCode SummonKey => Instance.summonKey;
    public static float SummonEnergyPerMeter => Instance.summonEnergyPerMeter;
    public static float MaxHealth => Instance.maxHealth;

    public static void UpdateAllHoverbikes()
    {
        foreach (var hoverbike in GameObject.FindObjectsOfType<Hoverbike>())
        {
            HoverbikePatches.UpdateHoverbike(hoverbike);
        }
    }
}

#endif