using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[System.Serializable]
public class EnemyReskinData
{
    public string EnemyName;
    public GameObject Prefab;

    public EnemyReskinConfigData ConfigData { get; private set; }

    public EnemyReskinData(string enemyName, GameObject prefab, EnemyReskinConfigDataDefault defaultConfigValues = default)
    {
        EnemyName = enemyName;
        Prefab = prefab;
        ConfigData = new EnemyReskinConfigData(defaultConfigValues);
    }

    public void BindConfigs()
    {
        ConfigData ??= new EnemyReskinConfigData();
        ConfigData.BindConfigs(this);
    }
}
