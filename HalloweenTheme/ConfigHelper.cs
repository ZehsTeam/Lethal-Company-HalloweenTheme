﻿using BepInEx.Configuration;
using com.github.zehsteam.HalloweenTheme.Dependencies;

namespace com.github.zehsteam.HalloweenTheme;

internal static class ConfigHelper
{
    public static void SkipAutoGen()
    {
        if (LethalConfigProxy.Enabled)
        {
            LethalConfigProxy.SkipAutoGen();
        }
    }

    public static ConfigEntry<T> Bind<T>(string section, string key, T defaultValue, bool requiresRestart, string description, AcceptableValueBase acceptableValues = null, System.Action<T> settingChanged = null, ConfigFile configFile = null)
    {
        configFile ??= Plugin.Instance.Config;

        var configEntry = acceptableValues == null
            ? configFile.Bind(section, key, defaultValue, description)
            : configFile.Bind(section, key, defaultValue, new ConfigDescription(description, acceptableValues));

        if (settingChanged != null)
        {
            configEntry.SettingChanged += (object sender, System.EventArgs e) => settingChanged?.Invoke(configEntry.Value);
        }

        if (LethalConfigProxy.Enabled)
        {
            if (acceptableValues == null)
            {
                LethalConfigProxy.AddConfig(configEntry, requiresRestart);
            }
            else
            {
                LethalConfigProxy.AddConfigSlider(configEntry, requiresRestart);
            }
        }

        return configEntry;
    }

    public static void AddButton(string section, string name, string description, string buttonText, System.Action callback)
    {
        if (LethalConfigProxy.Enabled)
        {
            LethalConfigProxy.AddButton(section, name, description, buttonText, callback);
        }
    }
}
