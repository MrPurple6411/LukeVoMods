namespace FasterGrowth;

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

	public static bool Loaded => Instance != null;

	[JsonProperty("DurationMultiplier")]
    [Slider("Duration Multiplier", 0.01f, 4f, DefaultValue = 0.01f, Step = 0.01f)]
    public float durationMultiplier = .01f;

    public static float DurationMultiplier => Instance.durationMultiplier;
}
