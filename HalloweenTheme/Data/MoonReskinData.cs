using System.Collections.Generic;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[System.Serializable]
public class MoonReskinData
{
    public string PlanetName;
    public GameObject Prefab;
    public List<SpawnableOutsideObjectWithRarity> SpawnableOutsideObjects = [];
    public bool DisableWeatherOverride;

    [field: SerializeField]
    public MoonReskinConfigData ConfigData { get; private set; }

    public MoonReskinData(string planetName, GameObject levelPrefab, List<SpawnableOutsideObjectWithRarity> spawnableOutsideObjects, MoonReskinConfigDataDefault defaultConfigValues = default)
    {
        PlanetName = planetName;
        Prefab = levelPrefab;
        SpawnableOutsideObjects = spawnableOutsideObjects;
        ConfigData = new MoonReskinConfigData(defaultConfigValues);
    }

    public void BindConfigs()
    {
        if (string.IsNullOrWhiteSpace(PlanetName))
        {
            return;
        }

        ConfigData ??= new MoonReskinConfigData();
        ConfigData.BindConfigs(this);
    }
}
