namespace OPBlade;

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

	public static bool Loaded => Instance != null;

	[JsonProperty("KnifeRange")]
    [Slider("Knife Range", 1f, 200f, DefaultValue = 10f, Step = 1f)]
    public float knifeRange = 10f;

    [JsonProperty("KnifeDamage")]
    [Slider("Knife Damage", 1f, 10000f, DefaultValue = 80f, Step = 1f)]
    public float knifeDamage = 80f;

    [JsonProperty("KnifeSpikyTrapDamage")]
    [Slider("Knife Spiky Trap Damage", 1f, 100f, DefaultValue = 4f, Step = 1f)]
    public float knifeSpikyTrapDamage = 4f;

    [JsonProperty("HeatBladeRange")]
    [Slider("Heat Blade Range", 1f, 200f, DefaultValue = 10f, Step = 1f)]
    public float heatBladeRange = 10f;

    [JsonProperty("HeatBladeDamage")]
    [Slider("Heat Blade Damage", 1f, 10000f, DefaultValue = 40f, Step = 1f)]
    public float heatBladeDamage = 40f;

    [JsonProperty("HeatBladeSpikyTrapDamage")]
    [Slider("Heat Blade Spiky Trap Damage", 1f, 100f, DefaultValue = 8f, Step = 1f)]
    public float heatBladeSpikyTrapDamage = 8f;

    public static float KnifeRange => Instance.knifeRange;
    public static float KnifeDamage => Instance.knifeDamage;
    public static float KnifeSpikyTrapDamage => Instance.knifeSpikyTrapDamage;
    public static float HeatBladeRange => Instance.heatBladeRange;
    public static float HeatBladeDamage => Instance.heatBladeDamage;
    public static float HeatBladeSpikyTrapDamage => Instance.heatBladeSpikyTrapDamage;

}