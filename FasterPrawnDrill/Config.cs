namespace FasterPrawnDrill;

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

	public static bool Loaded => Instance != null;

	[JsonProperty("MaxDrillHealth")]
    [Slider("Max Drill Health", 1f, 100f, DefaultValue = 50f, Step = 1f)]
    public float maxDrillHealth = 50f;

    [JsonProperty("AddOtherDamage")]
    [Slider("Add Other Damage", 1f, 100f, DefaultValue = 16f, Step = 1f)]
    public float addOtherDamage = 16f;

    public static float MaxDrillHealth => Instance.maxDrillHealth;
    public static float AddOtherDamage => Instance.addOtherDamage;
}
