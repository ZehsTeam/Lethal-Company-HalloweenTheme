using System;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[Serializable]
public class ItemReskinData
{
    public string ItemName;
    public GameObject Prefab;
    public ItemReskinConfigData ConfigData;

    public ItemReskinData(string itemName, GameObject prefab, ItemReskinConfigDataDefault defaultConfigValues = default)
    {
        ItemName = itemName;
        Prefab = prefab;
        ConfigData = new ItemReskinConfigData(defaultConfigValues);
    }

    public void BindConfigs()
    {
        if (ConfigData == null)
        {
            ConfigData = new ItemReskinConfigData();
        }

        ConfigData.BindConfigs(ItemName);
    }
}
