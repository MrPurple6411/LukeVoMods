namespace PropulseEverything;

using Nautilus.Json;
using Nautilus.Options.Attributes;
using Newtonsoft.Json;

[Menu(MyPluginInfo.PLUGIN_NAME)]
public class Config : ConfigFile
{
    public static Config Instance {get;} = Nautilus.Handlers.OptionsPanelHandler.RegisterModOptions<Config>();

	public static bool Loaded => Instance != null;

	[JsonProperty("EnergyRate")]
    [Slider("Energy Rate", 0.01f, 1f, DefaultValue = 0.5f, Step = 0.01f)]
    public float energyRate = .5f;

    [JsonProperty("PickupDistance")]
    [Slider("Pickup Distance", 10f, 100f, DefaultValue = 50f, Step = 1f)]
    public float pickupDistance = 50f;

    [JsonProperty("AttractionForce")]
    [Slider("Attraction Force", 100f, 1000f, DefaultValue = 350f, Step = 1f)]
    public float attractionForce = 350f;

    [JsonProperty("ShootForce")]
    [Slider("Shoot Force", 100f, 1000f, DefaultValue = 150f, Step = 1f)]
    public float shootForce = 150f;

    [JsonProperty("MassScalingFactor")]
    [Slider("Mass Scaling Factor", 0.001f, 0.01f, DefaultValue = 0.001f, Step = 0.001f)]
    public float massScalingFactor = 0.001f;

    public static float EnergyRate => Instance.energyRate;
    public static float PickupDistance => Instance.pickupDistance;
    public static float AttractionForce => Instance.attractionForce;
    public static float ShootForce => Instance.shootForce;
    public static float MassScalingFactor => Instance.massScalingFactor;

}