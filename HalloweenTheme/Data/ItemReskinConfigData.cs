using BepInEx.Configuration;

namespace com.github.zehsteam.HalloweenTheme.Data;

[System.Serializable]
public class ItemReskinConfigData
{
    public ItemReskinConfigDataDefault DefaultValues { get; private set; }

    public ConfigEntry<bool> Enabled { get; private set; }

    public ItemReskinData ItemReskinData { get; private set; }

    public ItemReskinConfigData()
    {
        DefaultValues = new ItemReskinConfigDataDefault();
    }

    public ItemReskinConfigData(ItemReskinConfigDataDefault defaultValues)
    {
        DefaultValues = defaultValues;
    }

    public void BindConfigs(ItemReskinData itemReskinData)
    {
        DefaultValues ??= new ItemReskinConfigDataDefault();

        if (itemReskinData == null)
        {
            return;
        }

        ItemReskinData = itemReskinData;

        string section = $"Item Reskins";

        Enabled = ConfigHelper.Bind(section, $"{ItemReskinData.ItemName} | Enabled", defaultValue: DefaultValues.Enabled, requiresRestart: true, $"Enable {ItemReskinData.ItemName} reskin. (Requires a full game restart for changes to fully apply!)");
    }
}

[System.Serializable]
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
