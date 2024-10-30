using com.github.zehsteam.HalloweenTheme.Data;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class LevelManager : MonoBehaviour
{
    public NavMeshData NavMeshData;
    public GameObject BrightDayPrefab;
    public string[] ObjectsToDisable = [];

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
            Plugin.Logger.LogError($"Failed to set NavMeshData. NavMeshSurface is null.");
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

        MoonReskinData moonReskinData = Content.HalloweenAssets.GetMoonReskinData(LevelHelper.PlanetName);
        if (moonReskinData == null) return;

        if (moonReskinData.DisableWeatherOverride)
        {
            return;
        }

        if (!moonReskinData.ConfigData.OverrideWeather.Value)
        {
            return;
        }

        if (LevelHelper.CurrentWeather == LevelWeatherType.Foggy)
        {
            return;
        }

        if (!Utils.RandomPercent(moonReskinData.ConfigData.OverrideWeatherChance.Value, new System.Random(LevelHelper.RandomMapSeed + 234)))
        {
            return;
        }

        GameObject lightingObject = GameObject.Find("Environment/Lighting");

        if (lightingObject == null)
        {
            Plugin.Logger.LogError("Failed to set bright day prefab. Lighting GameObject could not be found.");
            return;
        }

        GameObject oldBrightDayObject = lightingObject.transform.Find("BrightDay").gameObject;

        if (oldBrightDayObject == null)
        {
            Plugin.Logger.LogError("Failed to set bright day prefab. BrightDay GameObject could not be found.");
            return;
        }

        oldBrightDayObject.SetActive(false);

        GameObject brightDayObject = Instantiate(BrightDayPrefab, lightingObject.transform);
        brightDayObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        brightDayObject.name = brightDayObject.name.Replace("Clone", "").Trim();
        oldBrightDayObject.name = $"{oldBrightDayObject.name} (Old)";
        
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
            Plugin.Logger.LogError($"Failed to get NavMeshSurface.\n\n{e}");
        }

        return null;
    }
}
