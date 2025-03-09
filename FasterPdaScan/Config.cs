namespace FasterPdaScan;

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

    public static bool Loaded => Instance != null;

    [JsonProperty("ScanTimeMultiplier")]
    [Slider("Scan Time Multiplier", 0.01f, 4f, DefaultValue = 0.5f, Step = 0.01f)]
    public float scanTimeMultiplier = .5f;

    public static float ScanTimeMultiplier => Instance.scanTimeMultiplier;
}
