using BepInEx.Configuration;
using System.Collections.Generic;
using System.Reflection;

namespace com.github.zehsteam.HalloweenTheme;

internal class ConfigManager
{
    // General Settings
    public ConfigEntry<bool> ExtendedLogging;

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
