using com.github.zehsteam.HalloweenTheme.Data;
using com.github.zehsteam.HalloweenTheme.MonoBehaviours;
using HarmonyLib;
using System.Collections.Generic;

namespace com.github.zehsteam.HalloweenTheme.Patches;

[HarmonyPatch(typeof(StartOfRound))]
internal static class StartOfRoundPatch
{
    private static bool _initializedCustomAssets;

    [HarmonyPatch(nameof(StartOfRound.Start))]
    [HarmonyPostfix]
    private static void StartPatch()
    {
        InitializeCustomAssets();
    }

    private static void InitializeCustomAssets()
    {
        if (_initializedCustomAssets) return;
        _initializedCustomAssets = true;

        ReplaceSpawnableOutsideObjects();
        ApplyItemReskinData();
    }

    private static void ReplaceSpawnableOutsideObjects()
    {
        foreach (var level in StartOfRound.Instance.levels)
        {
            ReplaceSpawnableOutsideObjectsForLevel(level);
        }
    }

    private static void ReplaceSpawnableOutsideObjectsForLevel(SelectableLevel selectableLevel)
    {
        List<SpawnableOutsideObjectWithRarity> spawnableOutsideObjects = Content.HalloweenAssets.DefaultSpawnableOutsideObjects;

        LevelData levelData = LevelHelper.GetLevelData();
        
        if (levelData != null)
        {
            spawnableOutsideObjects = levelData.SpawnableOutsideObjects;
        }

        foreach (var newSpawnableOutsideObject in spawnableOutsideObjects)
        {
            bool hasSpawnableOutsideObject = false;

            for (int i = 0; i < selectableLevel.spawnableOutsideObjects.Length; i++)
            {
                if (selectableLevel.spawnableOutsideObjects[i].spawnableObject.name == newSpawnableOutsideObject.spawnableObject.name)
                {
                    hasSpawnableOutsideObject = true;
                    selectableLevel.spawnableOutsideObjects[i] = newSpawnableOutsideObject;
                    break;
                }
            }

            if (!hasSpawnableOutsideObject)
            {
                selectableLevel.spawnableOutsideObjects.AddItem(newSpawnableOutsideObject);
                Plugin.Instance.LogInfoExtended($"Added missing SpawnableOutsideObject \"{newSpawnableOutsideObject.spawnableObject.name}\" to level \"{selectableLevel.PlanetName}\".");
            }
        }

        Plugin.Instance.LogInfoExtended($"Replaced SpawnableOutsideObjects for level \"{selectableLevel.PlanetName}\".");
    }

    private static void ApplyItemReskinData()
    {
        foreach (var itemReskinData in Content.HalloweenAssets.ItemReskinDataList)
        {
            if (itemReskinData.Prefab == null) continue;

            if (!itemReskinData.ConfigData.Enabled.Value)
            {
                continue;
            }

            foreach (var item in StartOfRound.Instance.allItemsList.itemsList)
            {
                if (item.itemName != itemReskinData.ItemName) continue;

                ItemReskin itemReskin = itemReskinData.Prefab.GetComponent<ItemReskin>();
                if (itemReskin == null) continue;

                if (itemReskin.ItemProperties == null || !itemReskin.ItemProperties.Enabled)
                {
                    continue;
                }

                ItemHelper.SetItemProperties(item, itemReskin.ItemProperties.ToItemProperties());
            }
        }
    }

    [HarmonyPatch(nameof(StartOfRound.ShipHasLeft))]
    [HarmonyPostfix]
    private static void ShipHasLeftPatch()
    {
        Plugin.Instance.OnShipHasLeft();
    }

    [HarmonyPatch(nameof(StartOfRound.OnLocalDisconnect))]
    [HarmonyPostfix]
    private static void OnLocalDisconnectPatch()
    {
        Plugin.Instance.OnLocalDisconnect();
    }
}
