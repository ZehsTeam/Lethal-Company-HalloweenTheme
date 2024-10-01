using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public NavMeshData NavMeshData;
    public GameObject BrightDayPrefab;
    public string[] ObjectsToDisable = [];

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        DisableObjects();
        SetNavMeshData();
        SetBrightDay();
    }

    private void SetNavMeshData()
    {
        if (NavMeshData == null) return;

        NavMeshSurface navMeshSurface = GetNavMeshSurface();

        if (navMeshSurface == null)
        {
            Plugin.logger.LogError($"Failed to set NavMeshData. NavMeshSurface is null.");
            return;
        }

        navMeshSurface.navMeshData = NavMeshData;
        navMeshSurface.BuildNavMesh();

        Plugin.Instance.LogInfoExtended("Successfully set NavMeshData.");
    }

    private void DisableObjects()
    {
        if (ObjectsToDisable.Length == 0) return;

        foreach (var name in ObjectsToDisable)
        {
            Utils.DisableGameObject(name);
        }
    }

    private void SetBrightDay()
    {
        if (BrightDayPrefab == null) return;

        GameObject lightingObject = GameObject.Find("Environment/Lighting");

        if (lightingObject == null)
        {
            Plugin.logger.LogError("Failed to set bright day prefab. Lighting GameObject could not be found.");
            return;
        }

        GameObject oldBrightDayObject = lightingObject.transform.Find("BrightDay").gameObject;

        if (oldBrightDayObject == null)
        {
            Plugin.logger.LogError("Failed to set bright day prefab. BrightDay GameObject could not be found.");
            return;
        }

        oldBrightDayObject.SetActive(false);

        GameObject brightDayObject = Instantiate(BrightDayPrefab, lightingObject.transform);
        brightDayObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        Plugin.Instance.LogInfoExtended("Set BrightDay from prefab.");
    }

    private static NavMeshSurface GetNavMeshSurface()
    {
        try
        {
            return GameObject.Find("Environment").GetComponentInChildren<NavMeshSurface>();
        }
        catch (System.Exception e)
        {
            Plugin.logger.LogError($"Failed to get NavMeshSurface.\n\n{e}");
        }

        return null;
    }
}
