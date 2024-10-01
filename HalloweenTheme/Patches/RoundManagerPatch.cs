using HarmonyLib;

namespace com.github.zehsteam.HalloweenTheme.Patches;

[HarmonyPatch(typeof(RoundManager))]
internal static class RoundManagerPatch
{
    [HarmonyPatch(nameof(RoundManager.FinishGeneratingNewLevelClientRpc))]
    [HarmonyPostfix]
    static void FinishGeneratingNewLevelClientRpcPatch()
    {
        Plugin.Instance.OnLevelLoaded();
    }
}
