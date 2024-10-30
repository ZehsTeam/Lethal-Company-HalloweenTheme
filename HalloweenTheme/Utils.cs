using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme;

internal static class Utils
{
    public static bool RandomPercent(float percent)
    {
        if (percent <= 0f) return false;
        if (percent >= 100f) return true;

        return Random.value * 100f < percent;
    }

    public static bool RandomPercent(float percent, System.Random random)
    {
        if (percent <= 0f) return false;
        if (percent >= 100f) return true;

        return random.NextDouble() * 100f < percent;
    }

    public static void DisableGameObject(string name)
    {
        GameObject gameObject = GameObject.Find(name);

        if (gameObject == null)
        {
            Plugin.Logger.LogWarning($"Failed to disable GameObject \"{name}\". GameObject could not be found.");
            return;
        }

        gameObject.SetActive(false);
    }

    public static void HideGameObject(string name, bool disableColliders = true)
    {
        GameObject gameObject = GameObject.Find(name);

        if (gameObject == null)
        {
            Plugin.Logger.LogWarning($"Failed to hide GameObject \"{name}\". GameObject could not be found.");
            return;
        }

        HideGameObject(gameObject, disableColliders);
    }

    public static void HideGameObject(GameObject gameObject, bool disableColliders = true)
    {
        if (gameObject == null)
        {
            Plugin.Logger.LogWarning($"Failed to hide GameObject. GameObject could not be found.");
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
            Plugin.Logger.LogWarning($"Failed to disable colliders on GameObject \"{name}\". GameObject could not be found.");
            return;
        }

        DisableColliders(gameObject);
    }

    public static void DisableColliders(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Plugin.Logger.LogWarning($"Failed to disable colliders on GameObject. GameObject is null.");
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

        renderer.enabled = false;
    }

    public static void EnableRenderer(Renderer renderer)
    {
        if (renderer == null) return;
        //if (renderer.gameObject.layer == 9) return; // InteractableObject
        //if (renderer.gameObject.layer == 13) return; // Triggers
        //if (renderer.gameObject.layer == 22) return; // ScanNode

        renderer.enabled = true;
    }
}
