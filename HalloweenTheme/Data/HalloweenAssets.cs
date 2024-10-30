using System.Collections.Generic;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[CreateAssetMenu(fileName = "HalloweenAssets", menuName = "HalloweenTheme/HalloweenAssets")]
public class HalloweenAssets : ScriptableObject
{
    public List<MoonReskinData> MoonReskinDataList = [];
    public List<ItemReskinData> ItemReskinDataList = [];
    public List<EnemyReskinData> EnemyReskinDataList = [];
    public List<SpawnableOutsideObjectWithRarity> DefaultSpawnableOutsideObjects = [];

    internal void Initialize()
    {
        foreach (var itemReskinData in ItemReskinDataList)
        {
            itemReskinData.BindConfigs();
        }

        foreach (var enemyReskinData in EnemyReskinDataList)
        {
            enemyReskinData.BindConfigs();
        }

        foreach (var moonReskinData in MoonReskinDataList)
        {
            moonReskinData.BindConfigs();
        }
    }

    public MoonReskinData GetMoonReskinData(string planetName)
    {
        if (string.IsNullOrWhiteSpace(planetName))
        {
            return null;
        }

        foreach (var moonReskinData in MoonReskinDataList)
        {
            if (moonReskinData.Prefab == null) continue;

            if (moonReskinData.PlanetName == planetName)
            {
                return moonReskinData;
            }
        }

        return null;
    }

    public bool TryGetMoonReskinData(string planetName, out MoonReskinData moonReskinData)
    {
        moonReskinData = GetMoonReskinData(planetName);
        return moonReskinData != null;
    }

    public ItemReskinData GetItemReskinData(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            return null;
        }

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

    public EnemyReskinData GetEnemyReskinData(string enemyName)
    {
        if (string.IsNullOrWhiteSpace(enemyName))
        {
            return null;
        }

        foreach (var enemyReskinData in EnemyReskinDataList)
        {
            if (enemyReskinData.Prefab == null) continue;

            if (enemyReskinData.EnemyName == enemyName)
            {
                return enemyReskinData;
            }
        }

        return null;
    }

    public bool TryGetEnemyReskinData(string enemyName, out EnemyReskinData enemyReskinData)
    {
        enemyReskinData = GetEnemyReskinData(enemyName);
        return enemyReskinData != null;
    }
}
