using BepInEx.Configuration;
using System.Collections.Generic;
using System.Reflection;

namespace com.github.zehsteam.HalloweenTheme;

internal class ConfigManager
{
    // General Settings
    public ConfigEntry<bool> ExtendedLogging;

    // Moon Settings
    public ConfigEntry<bool> OverrideWeather;

    public ConfigManager()
    {
        BindConfigs();
        //ClearUnusedEntries();
    }

    private void BindConfigs()
    {
        ConfigHelper.SkipAutoGen();

        // General Settings
        ExtendedLogging = ConfigHelper.Bind("General Settings", "ExtendedLogging", defaultValue: false, requiresRestart: false, "Enable extended logging.");

        // Moon Settings
        OverrideWeather = ConfigHelper.Bind("Moon Settings", "OverrideWeather", defaultValue: true, requiresRestart: false, "If enabled, some vanilla moons will have a chance to be dark and foggy.");
    }

    private void ClearUnusedEntries()
    {
        ConfigFile configFile = Plugin.Instance.Config;

        // Normally, old unused config entries don't get removed, so we do it with this piece of code. Credit to Kittenji.
        PropertyInfo orphanedEntriesProp = configFile.GetType().GetProperty("OrphanedEntries", BindingFlags.NonPublic | BindingFlags.Instance);
        var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp.GetValue(configFile, null);
        orphanedEntries.Clear(); // Clear orphaned entries (Unbinded/Abandoned entries)
        configFile.Save(); // Save the config file to save these changes
    }
}
