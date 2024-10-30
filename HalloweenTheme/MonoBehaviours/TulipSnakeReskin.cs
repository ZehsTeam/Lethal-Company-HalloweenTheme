using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class TulipSnakeReskin : MonoBehaviour
{
    public GameObject ModelObject;
    public Vector3 Position;
    public Vector3 Rotation;
    public ParticleSystem ParticleSystem;

    public FlowerSnakeEnemy FlowerSnakeEnemy { get; private set; }

    private bool _isDead;

    private void Start()
    {
        if (transform.parent == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!transform.parent.TryGetComponent(out FlowerSnakeEnemy flowerSnakeEnemy))
        {
            Destroy(gameObject);
            return;
        }
        
        FlowerSnakeEnemy = flowerSnakeEnemy;

        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        HideOriginalModel();
        SetModelParent();
    }

    private void HideOriginalModel()
    {
        if (FlowerSnakeEnemy == null || ModelObject == null)
        {
            return;
        }

        foreach (var skinnedMeshRenderer in FlowerSnakeEnemy.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            skinnedMeshRenderer.enabled = false;
        }
    }

    private void SetModelParent()
    {
        if (FlowerSnakeEnemy == null || ModelObject == null)
        {
            return;
        }

        try
        {
            Transform parentTransform = FlowerSnakeEnemy.transform.Find("FlowerLizardModel").Find("AnimContainer").Find("Armature").Find("Belly").Find("LowerChest");

            ModelObject.transform.SetParent(parentTransform);
            ModelObject.transform.SetLocalPositionAndRotation(Position, Quaternion.Euler(Rotation));

            Plugin.Instance.LogInfoExtended("Set model parent.");
        }
        catch (System.Exception ex)
        {
            Plugin.Logger.LogError($"Failed to set model parent. {ex}");
        }
    }

    private void Update()
    {
        if (FlowerSnakeEnemy == null) return;

        if (FlowerSnakeEnemy.isEnemyDead && !_isDead)
        {
            _isDead = true;
            OnDeath();
        }
    }

    private void OnDeath()
    {
        if (ParticleSystem == null) return;

        ParticleSystem.Stop();
    }
}
