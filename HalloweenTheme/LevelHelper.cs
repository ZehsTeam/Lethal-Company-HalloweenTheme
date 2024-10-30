using com.github.zehsteam.HalloweenTheme.Data;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme;

internal static class LevelHelper
{
    private static bool _spawnedHalloweenLevelPrefab;

    public static string PlanetName => StartOfRound.Instance.currentLevel.PlanetName;
    public static LevelWeatherType CurrentWeather => StartOfRound.Instance.currentLevel.currentWeather;
    public static int RandomMapSeed => StartOfRound.Instance.randomMapSeed;

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
        MoonReskinData moonReskinData = Content.HalloweenAssets.GetMoonReskinData(PlanetName);
        if (moonReskinData == null) return;

        if (!moonReskinData.ConfigData.Enabled.Value)
        {
            return;
        }

        GameObject environmentObject = GameObject.Find("Environment");

        if (environmentObject == null)
        {
            Plugin.Logger.LogError("Failed to spawn halloween level prefab. Environment GameObject could not be found.");
            return;
        }

        GameObject halloweenLevel = Object.Instantiate(moonReskinData.Prefab, environmentObject.transform);
        halloweenLevel.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        Plugin.Instance.LogInfoExtended($"Spawned halloween level prefab \"{moonReskinData.Prefab.name}\".");
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
