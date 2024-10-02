using BepInEx.Configuration;
using System;

namespace com.github.zehsteam.HalloweenTheme.Data;

[Serializable]
public class ItemReskinConfigData
{
    public ItemReskinConfigDataDefault DefaultValues { get; private set; }

    public ConfigEntry<bool> Enabled { get; private set; }

    public ItemReskinConfigData()
    {
        DefaultValues = new ItemReskinConfigDataDefault();
    }

    public ItemReskinConfigData(ItemReskinConfigDataDefault defaultValues)
    {
        DefaultValues = defaultValues;
    }

    public void BindConfigs(string itemName)
    {
        DefaultValues ??= new ItemReskinConfigDataDefault();

        string section = $"{itemName} Reskin Settings";

        Enabled = ConfigHelper.Bind(section, "Enabled", defaultValue: DefaultValues.Enabled, requiresRestart: true, $"Enable {itemName} reskin. Requires a full game restart for changes to apply.");
    }
}

[Serializable]
public class ItemReskinConfigDataDefault
{
    public bool Enabled = true;

    public ItemReskinConfigDataDefault()
    {

    }

    public ItemReskinConfigDataDefault(bool enabled)
    {
        Enabled = enabled;
    }
}
