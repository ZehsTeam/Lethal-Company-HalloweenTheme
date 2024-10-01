using com.github.zehsteam.HalloweenTheme.Data;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme;

internal static class LevelHelper
{
    private static bool _spawnedHalloweenLevelPrefab;

    public static string PlanetName => StartOfRound.Instance.currentLevel.PlanetName;

    public static void Reset()
    {
        _spawnedHalloweenLevelPrefab = false;
    }

    public static void OnLevelLoaded()
    {
        if (_spawnedHalloweenLevelPrefab) return;
        _spawnedHalloweenLevelPrefab = true;

        SpawnHalloweenLevelPrefab();
    }

    private static void SpawnHalloweenLevelPrefab()
    {
        LevelData levelData = GetLevelData();
        if (levelData == null) return;
        if (levelData.LevelPrefab == null) return;

        GameObject environmentObject = GameObject.Find("Environment");

        if (environmentObject == null)
        {
            Plugin.logger.LogError("Failed to spawn halloween level prefab. Environment GameObject could not be found.");
            return;
        }

        GameObject halloweenLevel = Object.Instantiate(levelData.LevelPrefab, environmentObject.transform);
        halloweenLevel.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        Plugin.Instance.LogInfoExtended($"Spawned halloween level prefab \"{levelData.LevelPrefab.name}\".");
    }

    public static LevelData GetLevelData()
    {
        foreach (var levelData in Content.HalloweenAssets.LevelDataList)
        {
            if (levelData.PlanetName == PlanetName)
            {
                return levelData;
            }
        }

        return null;
    }

    public static EntranceTeleport GetEntranceTeleport(int entranceId, bool isEntranceToBuilding = true)
    {
        foreach (var entranceTeleport in Object.FindObjectsByType<EntranceTeleport>(FindObjectsSortMode.None))
        {
            if (isEntranceToBuilding && !entranceTeleport.isEntranceToBuilding) continue;

            if (entranceTeleport.entranceId == entranceId)
            {
                return entranceTeleport;
            }
        }

        return null;
    }
}
