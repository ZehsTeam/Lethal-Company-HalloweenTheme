using BepInEx.Configuration;

namespace com.github.zehsteam.HalloweenTheme.Data;

[System.Serializable]
public class EnemyReskinConfigData
{
    public EnemyReskinConfigDataDefault DefaultValues { get; private set; }

    public ConfigEntry<bool> Enabled { get; private set; }

    public EnemyReskinData EnemyReskinData { get; private set; }

    public EnemyReskinConfigData()
    {
        DefaultValues = new EnemyReskinConfigDataDefault();
    }

    public EnemyReskinConfigData(EnemyReskinConfigDataDefault defaultValues)
    {
        DefaultValues = defaultValues;
    }

    public void BindConfigs(EnemyReskinData enemyReskinData)
    {
        DefaultValues ??= new EnemyReskinConfigDataDefault();

        if (enemyReskinData == null)
        {
            return;
        }

        EnemyReskinData = enemyReskinData;

        string section = $"Enemy Reskins";

        Enabled = ConfigHelper.Bind(section, $"{EnemyReskinData.EnemyName} | Enabled", defaultValue: DefaultValues.Enabled, requiresRestart: false, $"Enable {EnemyReskinData.EnemyName} reskin.");
    }
}

[System.Serializable]
public class EnemyReskinConfigDataDefault
{
    public bool Enabled = true;

    public EnemyReskinConfigDataDefault()
    {

    }

    public EnemyReskinConfigDataDefault(bool enabled)
    {
        Enabled = enabled;
    }
}
