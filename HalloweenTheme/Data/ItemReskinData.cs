using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[System.Serializable]
public class ItemReskinData
{
    public string ItemName;
    public GameObject Prefab;

    public ItemReskinConfigData ConfigData { get; private set; }

    public ItemReskinData(string itemName, GameObject prefab, ItemReskinConfigDataDefault defaultConfigValues = default)
    {
        ItemName = itemName;
        Prefab = prefab;
        ConfigData = new ItemReskinConfigData(defaultConfigValues);
    }

    public void BindConfigs()
    {
        ConfigData ??= new ItemReskinConfigData();
        ConfigData.BindConfigs(this);
    }
}
