using System.Collections.Generic;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[CreateAssetMenu(fileName = "HalloweenAssets", menuName = "HalloweenTheme/HalloweenAssets")]
public class HalloweenAssets : ScriptableObject
{
    public List<LevelData> LevelDataList = [];
    public List<SpawnableOutsideObjectWithRarity> DefaultSpawnableOutsideObjects = [];
    public List<ItemReskinData> ItemReskinDataList = [];

    internal void Initialize()
    {
        foreach (var itemReskinData in ItemReskinDataList)
        {
            itemReskinData.BindConfigs();
        }
    }

    public ItemReskinData GetItemReskinData(string itemName)
    {
        foreach (var itemReskinData in ItemReskinDataList)
        {
            if (itemReskinData.Prefab == null) continue;

            if (itemReskinData.ItemName == itemName)
            {
                return itemReskinData;
            }
        }

        return null;
    }

    public bool TryGetItemReskinData(string itemName, out ItemReskinData itemReskinData)
    {
        itemReskinData = GetItemReskinData(itemName);
        return itemReskinData != null;
    }
}
