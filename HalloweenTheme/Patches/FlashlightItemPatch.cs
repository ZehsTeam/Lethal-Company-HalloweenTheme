using com.github.zehsteam.HalloweenTheme.MonoBehaviours;
using HarmonyLib;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Patches;

[HarmonyPatch(typeof(FlashlightItem))]
internal class FlashlightItemPatch
{
    [HarmonyPatch(nameof(FlashlightItem.ItemActivate))]
    [HarmonyPrefix]
    private static bool ItemActivatePatch(ref FlashlightItem __instance, bool used)
    {
        FlashlightReskin flashlightReskin = __instance.GetComponentInChildren<FlashlightReskin>();
        if (flashlightReskin == null) return true;

        AudioClip audioClip = used ? flashlightReskin.TurnOnSFX : flashlightReskin.TurnOffSFX;
        if (audioClip == null) return true;

        if (__instance.flashlightInterferenceLevel < 2)
        {
            __instance.SwitchFlashlight(used);
        }

        __instance.flashlightAudio.PlayOneShot(audioClip);
        RoundManager.Instance.PlayAudibleNoise(__instance.transform.position, 7f, 0.4f, 0, __instance.isInElevator && StartOfRound.Instance.hangarDoorsClosed);

        return false;
    }

    [HarmonyPatch(nameof(FlashlightItem.EquipItem))]
    [HarmonyPostfix]
    private static void EquipItemPatch(ref FlashlightItem __instance)
    {
        if (__instance.IsOwner) return;
        if (__instance.playerHeldBy == null) return;
        if (!__instance.isBeingUsed) return;

        FlashlightReskin flashlightReskin = __instance.GetComponentInChildren<FlashlightReskin>();
        if (flashlightReskin == null) return;

        if (flashlightReskin.IsTorch)
        {
            __instance.playerHeldBy.ChangeHelmetLight(__instance.flashlightTypeID, enable: false);
            __instance.flashlightBulb.enabled = true;
        }
    }

    [HarmonyPatch(nameof(FlashlightItem.SwitchFlashlight))]
    [HarmonyPostfix]
    private static void SwitchFlashlightPatch(ref FlashlightItem __instance, bool on)
    {
        FlashlightReskin flashlightReskin = __instance.GetComponentInChildren<FlashlightReskin>();
        if (flashlightReskin == null) return;

        flashlightReskin.SwitchFlashlight(on);
        
        if (__instance.IsOwner) return;
        if (__instance.playerHeldBy == null) return;
        if (__instance.isPocketed) return;

        if (flashlightReskin.IsTorch)
        {
            __instance.playerHeldBy.ChangeHelmetLight(__instance.flashlightTypeID, enable: false);
            __instance.flashlightBulb.enabled = on;
        }
    }
}
