using com.github.zehsteam.HalloweenTheme.Data;
using com.github.zehsteam.HalloweenTheme.MonoBehaviours;
using HarmonyLib;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Patches;

[HarmonyPatch(typeof(GrabbableObject))]
internal class GrabbableObjectPatch
{
    [HarmonyPatch(nameof(GrabbableObject.Start))]
    [HarmonyPostfix]
    private static void StartPatch(ref GrabbableObject __instance)
    {
        if (__instance.itemProperties == null) return;

        if (Content.HalloweenAssets.TryGetItemReskinData(__instance.itemProperties.itemName, out ItemReskinData itemReskinData))
        {
            if (!itemReskinData.ConfigData.Enabled.Value) return;

            Object.Instantiate(itemReskinData.Prefab, __instance.transform);
        }
    }

    [HarmonyPatch(nameof(GrabbableObject.EnableItemMeshes))]
    [HarmonyPostfix]
    private static void EnableItemMeshesPatch(ref GrabbableObject __instance)
    {
        ItemReskin itemReskin = __instance.GetComponentInChildren<ItemReskin>();
        if (itemReskin == null) return;

        itemReskin.HideOriginalModel();
    }
}
