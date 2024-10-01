using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class FlashlightReskin : ItemReskin
{
    public FlashlightItem FlashlightItem { get; private set; }
    public float InitialLightIntensity { get; private set; }

    [Space(20f)]
    [Header("Flashlight")]
    [Space(5f)]
    public Light Light;
    public bool IsTorch;
    public float FlickerSpeed = 10f;
    public float IntensityMultiplier = 0.2f;

    public override void Start()
    {
        base.Start();

        InitialLightIntensity = Light.intensity;
    }

    public override void LateStart()
    {
        base.LateStart();

        if (GrabbableObject == null) return;

        FlashlightItem = GrabbableObject as FlashlightItem;
        if (FlashlightItem == null) return;

        SetLight();
    }

    private void Update()
    {
        if (FlashlightItem == null) return;
        if (Light == null) return;
        if (!IsTorch) return;

        if (!FlashlightItem.isBeingUsed) return;

        float noise = Mathf.PerlinNoise(Time.time * FlickerSpeed, 0.0f);
        Light.intensity = Mathf.Lerp(InitialLightIntensity * (1f - IntensityMultiplier), InitialLightIntensity * (1f + IntensityMultiplier), noise);
    }

    private void SetLight()
    {
        if (FlashlightItem == null) return;
        if (Light == null) return;

        FlashlightItem.initialIntensity = InitialLightIntensity;

        if (FlashlightItem.flashlightBulb != null)
        {
            FlashlightItem.flashlightBulb.enabled = false;
        }

        if (FlashlightItem.flashlightBulbGlow != null)
        {
            FlashlightItem.flashlightBulbGlow.gameObject.SetActive(false);
        }

        FlashlightItem.flashlightBulb = Light;
    }
}
