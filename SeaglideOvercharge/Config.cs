namespace SeaglideOvercharge;

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;
using UnityEngine;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

	public static bool Loaded => Instance != null;

	[JsonProperty("OverchargeDuration")]
    [Slider("Overcharge Duration", 1f, 60f, DefaultValue = 15f, Step = 1f)]
    public float overchargeDuration = 15f;

    [JsonProperty("OverchargeBoost")]
    [Slider("Overcharge Boost", 1f, 10f, DefaultValue = 2.5f, Step = 0.1f)]
    public float overchargeBoost = 2.5f;

    [JsonProperty("OverchargeEnergyConsumption")]
    [Slider("Overcharge Energy Consumption", 1f, 100f, DefaultValue = 20f, Step = 1f)]
    public float overchargeEnergyConsumption = 20f;

    [JsonProperty("OverchargeKey")]
    [Keybind("Overcharge Key")]
    public KeyCode overchargeKey = KeyCode.T;

    public static float OverchargeDuration => Instance.overchargeDuration;
    public static float OverchargeBoost => Instance.overchargeBoost;
    public static float OverchargeEnergyConsumption => Instance.overchargeEnergyConsumption;
    public static KeyCode OverchargeKey => Instance.overchargeKey;

}
