﻿using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme;

internal static class Utils
{
    public static void DisableGameObject(string name)
    {
        GameObject gameObject = GameObject.Find(name);

        if (gameObject == null)
        {
            Plugin.logger.LogWarning($"Failed to disable GameObject \"{name}\". GameObject could not be found.");
            return;
        }

        gameObject.SetActive(false);
    }

    public static void HideGameObject(string name, bool disableColliders = true)
    {
        GameObject gameObject = GameObject.Find(name);

        if (gameObject == null)
        {
            Plugin.logger.LogWarning($"Failed to hide GameObject \"{name}\". GameObject could not be found.");
            return;
        }

        HideGameObject(gameObject, disableColliders);
    }

    public static void HideGameObject(GameObject gameObject, bool disableColliders = true)
    {
        if (gameObject == null)
        {
            Plugin.logger.LogWarning($"Failed to hide GameObject. GameObject could not be found.");
            return;
        }

        foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>())
        {
            DisableRenderer(renderer);
        }

        if (disableColliders)
        {
            DisableColliders(gameObject);
        }
    }

    public static void DisableColliders(string name)
    {
        GameObject gameObject = GameObject.Find(name);

        if (gameObject == null)
        {
            Plugin.logger.LogWarning($"Failed to disable colliders on GameObject \"{name}\". GameObject could not be found.");
            return;
        }

        DisableColliders(gameObject);
    }

    public static void DisableColliders(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Plugin.logger.LogWarning($"Failed to disable colliders on GameObject. GameObject is null.");
            return;
        }

        foreach (var collider in gameObject.GetComponentsInChildren<Collider>())
        {
            if (IgnoreCollider(collider)) continue;

            collider.enabled = false;
        }
    }

    public static bool IgnoreCollider(Collider collider)
    {
        if (collider.isTrigger) return true;
        if (collider.gameObject.CompareTag("InteractTrigger")) return true;
        if (collider.gameObject.layer == LayerMask.GetMask("InteractableObject")) return true;
        if (collider.gameObject.layer == LayerMask.GetMask("Trigger")) return true;
        
        return false;
    }

    public static void DisableRenderer(Renderer renderer)
    {
        if (renderer == null) return;
        //if (renderer.gameObject.layer == 22) return;
        //if (renderer.gameObject.CompareTag("DoNotSet")) return;
        //if (renderer.gameObject.CompareTag("InteractTrigger")) return;

        renderer.enabled = false;
    }
}