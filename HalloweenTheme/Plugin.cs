using BepInEx;
using BepInEx.Logging;
using com.github.zehsteam.HalloweenTheme.Patches;
using HarmonyLib;

namespace com.github.zehsteam.HalloweenTheme;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
internal class Plugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

    internal static Plugin Instance { get; private set; }
    internal static new ManualLogSource Logger { get; private set; }

    internal static ConfigManager ConfigManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        Logger = BepInEx.Logging.Logger.CreateLogSource(MyPluginInfo.PLUGIN_GUID);
        Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} has awoken!");

        harmony.PatchAll(typeof(StartOfRoundPatch));
        harmony.PatchAll(typeof(RoundManagerPatch));
        harmony.PatchAll(typeof(GrabbableObjectPatch));
        harmony.PatchAll(typeof(FlashlightItemPatch));
        harmony.PatchAll(typeof(EnemyAIPatch));
        
        ConfigManager = new ConfigManager();

        Content.Load();
        Content.HalloweenAssets.Initialize();
    }

    public void OnLevelLoaded()
    {
        LevelHelper.OnLevelLoaded();
    }

    public void OnShipHasLeft()
    {
        LevelHelper.Reset();
    }

    public void OnLocalDisconnect()
    {
        LevelHelper.Reset();
    }

    public void LogInfoExtended(object data)
    {
        if (ConfigManager.ExtendedLogging.Value)
        {
            Logger.LogInfo(data);
        }
    }
}
