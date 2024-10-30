using BepInEx.Configuration;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[System.Serializable]
public class MoonReskinConfigData
{
    [field: SerializeField]
    public MoonReskinConfigDataDefault DefaultValues { get; private set; }

    public ConfigEntry<bool> Enabled { get; private set; }
    public ConfigEntry<bool> OverrideWeather { get; private set; }
    public ConfigEntry<float> OverrideWeatherChance { get; private set; }

    public MoonReskinData MoonReskinData { get; private set; }

    public MoonReskinConfigData(MoonReskinConfigDataDefault defaultValues = default)
    {
        DefaultValues = defaultValues;
    }

    public void BindConfigs(MoonReskinData moonReskinData)
    {
        DefaultValues ??= new MoonReskinConfigDataDefault();

        if (moonReskinData == null)
        {
            return;
        }

        MoonReskinData = moonReskinData;

        string section = $"{MoonReskinData.PlanetName} Reskin";

        Enabled = ConfigHelper.Bind(section, $"Enabled", defaultValue: DefaultValues.Enabled, requiresRestart: false, $"Enable {MoonReskinData.PlanetName} reskin.");
        
        if (!MoonReskinData.DisableWeatherOverride)
        {
            OverrideWeather = ConfigHelper.Bind(section, $"OverrideWeather", defaultValue: DefaultValues.OverrideWeather, requiresRestart: false, $"If enabled, {MoonReskinData.PlanetName} will have a chance to have a custom dark and foggy weather applied based on OverrideWeatherChance.");
            OverrideWeatherChance = ConfigHelper.Bind(section, $"OverrideWeatherChance", defaultValue: DefaultValues.OverrideWeatherChance, requiresRestart: false, $"The percent chance a custom dark and foggy weather will be applied. (Requires OverrideWeather to be enabled)", new AcceptableValueRange<float>(0.0f, 100.0f));
        }
    }
}

[System.Serializable]
public class MoonReskinConfigDataDefault
{
    public bool Enabled = true;
    public bool OverrideWeather = true;
    public float OverrideWeatherChance = 50f;

    public MoonReskinConfigDataDefault()
    {

    }

    public MoonReskinConfigDataDefault(bool enabled, bool overrideWeather, float overrideWeatherChance)
    {
        Enabled = enabled;
        OverrideWeather = overrideWeather;
        OverrideWeatherChance = overrideWeatherChance;
    }
}
