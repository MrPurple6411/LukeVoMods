namespace OxyPlant;
#if BELOWZERO

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

	public static bool Loaded => Instance != null;

	[JsonProperty("Duration")]
    [Slider("Duration", 1f, 600f, DefaultValue = 60f, Step = 1f)]
    public float duration = 60f;

    [JsonProperty("Capacity")]
    [Slider("Capacity", 1f, 100f, DefaultValue = 70f, Step = 1f)]
    public float capacity = 70f;

    public static float Duration => Instance.duration;

    public static float Capacity => Instance.capacity;
}

#endif