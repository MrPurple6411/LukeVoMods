namespace ExtendedMineralDetector;

#if BELOWZERO
using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;
using UnityEngine;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

    public static bool Loaded => Instance != null;

    [JsonProperty("PowerConsumption")]
    [Slider("Power Consumption", 0.01f, 1f, DefaultValue = 0.5f, Step = 0.01f), OnChange(nameof(UpdateAllMetalDetectors))]
    public float powerConsumption = .5f;

    [JsonProperty("ScanDistance")]
    [Slider("Scan Distance", 100f, 500f, DefaultValue = 200f, Step = 1f), OnChange(nameof(UpdateAllMetalDetectors))]
    public float scanDistance = 200f;

    public static float PowerConsumption => Instance.powerConsumption;
    public static float ScanDistance => Instance.scanDistance;

    public void UpdateAllMetalDetectors()
    {
        foreach (var metalDetector in GameObject.FindObjectsOfType<MetalDetector>())
        {
            metalDetector.powerConsumption = PowerConsumption;
            metalDetector.scanDistance = ScanDistance;
        }
    }
}
#endif
