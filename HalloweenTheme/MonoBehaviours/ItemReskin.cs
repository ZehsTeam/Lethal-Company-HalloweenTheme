using com.github.zehsteam.HalloweenTheme.Data;
using System.Collections;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class ItemReskin : MonoBehaviour
{
    public GrabbableObject GrabbableObject { get; private set; }

    [Header("Transform")]
    [Space(5f)]
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale = Vector3.one;

    [Space(10f)]
    public ItemReskinScanNodeProperties ScanNodeProperties;

    [Space(10f)]
    public ItemReskinItemProperties ItemProperties;

    [Space(10f)]
    public ItemReskinBoxColliderProperties MainBoxColliderProperties;

    [Space(10f)]
    public ItemReskinTransformProperties ScanNodeTransformProperties;

    [Space(10f)]
    public ItemReskinBoxColliderProperties ScanNodeBoxColliderProperties;

    public virtual void Start()
    {
        if (transform.parent == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!transform.parent.TryGetComponent(out GrabbableObject grabbableObject))
        {
            Destroy(gameObject);
            return;
        }

        GrabbableObject = grabbableObject;

        transform.SetLocalPositionAndRotation(Position, Quaternion.Euler(Rotation));
        transform.localScale = Scale;

        StartCoroutine(LateStartCoroutine());
    }

    private IEnumerator LateStartCoroutine()
    {
        yield return null;
        LateStart();
    }

    public virtual void LateStart()
    {
        HideOriginalModel();
        SetItemProperties();
        SetScanNodeProperties();
        SetMainBoxColliderProperties();
        SetScanNodeTransformProperties();
        SetScanNodeBoxColliderProperties();
    }

    public void HideOriginalModel()
    {
        if (GrabbableObject == null) return;

        foreach (var renderer in GrabbableObject.GetComponents<Renderer>())
        {
            Utils.DisableRenderer(renderer);
        }

        for (int i = 0; i < GrabbableObject.transform.childCount; i++)
        {
            if (GrabbableObject.transform.GetChild(i) == transform) continue;

            foreach (var renderer in GrabbableObject.transform.GetChild(i).GetComponentsInChildren<Renderer>())
            {
                Utils.DisableRenderer(renderer);
            }
        }
    }

    private void SetItemProperties()
    {
        if (GrabbableObject == null) return;

        if (ItemProperties == null || !ItemProperties.Enabled)
        {
            return;
        }

        ItemHelper.SetItemProperties(GrabbableObject.itemProperties, ItemProperties.ToItemProperties());
    }

    private void SetScanNodeProperties()
    {
        if (GrabbableObject == null) return;

        if (ScanNodeProperties == null || string.IsNullOrWhiteSpace(ScanNodeProperties.HeaderText))
        {
            return;
        }

        ScanNodeProperties scanNodeProperties = GrabbableObject.GetComponentInChildren<ScanNodeProperties>();
        if (scanNodeProperties == null) return;

        scanNodeProperties.headerText = ScanNodeProperties.HeaderText;
    }

    private void SetMainBoxColliderProperties()
    {
        if (GrabbableObject == null) return;
        if (MainBoxColliderProperties == null || !MainBoxColliderProperties.Enabled) return;

        BoxCollider boxCollider = GrabbableObject.GetComponent<BoxCollider>();
        if (boxCollider == null) return;

        boxCollider.center = MainBoxColliderProperties.Center;
        boxCollider.size = MainBoxColliderProperties.Size;
    }

    private void SetScanNodeTransformProperties()
    {
        if (GrabbableObject == null) return;
        if (ScanNodeTransformProperties == null || !ScanNodeTransformProperties.Enabled) return;

        ScanNodeProperties scanNodeProperties = GrabbableObject.GetComponentInChildren<ScanNodeProperties>();
        if (scanNodeProperties == null) return;

        scanNodeProperties.transform.SetLocalPositionAndRotation(ScanNodeTransformProperties.Position, Quaternion.Euler(ScanNodeTransformProperties.Rotation));
        scanNodeProperties.transform.localScale = ScanNodeTransformProperties.Scale;
    }

    private void SetScanNodeBoxColliderProperties()
    {
        if (GrabbableObject == null) return;
        if (ScanNodeBoxColliderProperties == null || !ScanNodeBoxColliderProperties.Enabled) return;

        ScanNodeProperties scanNodeProperties = GrabbableObject.GetComponentInChildren<ScanNodeProperties>();
        if (scanNodeProperties == null) return;

        BoxCollider boxCollider = scanNodeProperties.GetComponent<BoxCollider>();
        if (boxCollider == null) return;

        boxCollider.center = ScanNodeBoxColliderProperties.Center;
        boxCollider.size = ScanNodeBoxColliderProperties.Size;
    }
}

[System.Serializable]
public class ItemReskinScanNodeProperties
{
    public string HeaderText;
}

[System.Serializable]
public class ItemReskinItemProperties
{
    public bool Enabled;

    [Space(10f)]
    public AudioClip GrabSFX;
    public AudioClip DropSFX;
    public AudioClip PocketSFX;
    public AudioClip ThrowSFX;
    public float VerticalOffset;
    public int FloorYOffset;
    public bool AllowDroppingAheadOfPlayer;
    public Vector3 RestingRotation;
    public Vector3 RotationOffset;
    public Vector3 PositionOffset;

    public ItemProperties ToItemProperties()
    {
        return new ItemProperties(GrabSFX, DropSFX, PocketSFX, ThrowSFX, VerticalOffset, FloorYOffset, AllowDroppingAheadOfPlayer, RestingRotation, RotationOffset, PositionOffset);
    }
}

[System.Serializable]
public class ItemReskinBoxColliderProperties
{
    public bool Enabled;

    [Space(10f)]
    public Vector3 Center;
    public Vector3 Size = Vector3.one;
}

[System.Serializable]
public class ItemReskinTransformProperties
{
    public bool Enabled;

    [Space(10f)]
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale = Vector3.one;
}
