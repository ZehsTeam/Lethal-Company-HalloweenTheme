using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class LampReskin : ItemReskin
{
    [Space(20f)]
    [Header("Lamp")]
    [Space(5f)]
    public Color Color;

    [Space(10f)]
    public ItemReskinTransformProperties LightTransformProperties;

    public override void LateStart()
    {
        base.LateStart();

        SetLightColor();
        SetLightTransformProperties();
    }

    private void SetLightColor()
    {
        if (GrabbableObject == null) return;

        Light light = GrabbableObject.GetComponentInChildren<Light>();
        if (light == null) return;

        light.color = Color;
    }

    private void SetLightTransformProperties()
    {
        if (GrabbableObject == null) return;
        if (LightTransformProperties == null || !LightTransformProperties.Enabled) return;

        Light light = GrabbableObject.GetComponentInChildren<Light>();
        if (light == null) return;

        light.transform.SetLocalPositionAndRotation(LightTransformProperties.Position, Quaternion.Euler(LightTransformProperties.Rotation));
        light.transform.localScale = LightTransformProperties.Scale;
    }
}
