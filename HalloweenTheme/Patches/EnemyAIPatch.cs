using com.github.zehsteam.HalloweenTheme.Data;
using HarmonyLib;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Patches;

[HarmonyPatch(typeof(EnemyAI))]
internal static class EnemyAIPatch
{
    [HarmonyPatch(nameof(EnemyAI.Start))]
    [HarmonyPostfix]
    private static void StartPatch(ref EnemyAI __instance)
    {
        SetEnemyReskin(__instance);
    }

    private static void SetEnemyReskin(EnemyAI enemyAI)
    {
        if (enemyAI == null || enemyAI.enemyType == null)
        {
            return;
        }

        if (Content.HalloweenAssets.TryGetEnemyReskinData(enemyAI.enemyType.enemyName, out EnemyReskinData enemyReskinData))
        {
            if (!enemyReskinData.ConfigData.Enabled.Value)
            {
                return;
            }

            Object.Instantiate(enemyReskinData.Prefab, enemyAI.transform);
        }
    }
}
