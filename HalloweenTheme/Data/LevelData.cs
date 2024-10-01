using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[Serializable]
public class LevelData
{
    public string PlanetName;
    public GameObject LevelPrefab;
    public List<SpawnableOutsideObjectWithRarity> SpawnableOutsideObjects = [];

    public LevelData(string planetName, GameObject levelPrefab, List<SpawnableOutsideObjectWithRarity> spawnableOutsideObjects)
    {
        PlanetName = planetName;
        LevelPrefab = levelPrefab;
        SpawnableOutsideObjects = spawnableOutsideObjects;
    }
}
