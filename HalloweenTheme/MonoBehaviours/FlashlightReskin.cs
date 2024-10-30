using System.Collections;
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
    public AudioClip TurnOnSFX;
    public AudioClip TurnOffSFX;
    public bool IsTorch;
    public float FlickerSpeed = 1.5f;
    public float IntensityMultiplier = 0.4f;
    public AudioSource TorchBurnAudio;

    private Coroutine _playTorchBurnAudioAfterDelayCoroutine;

    public override void Start()
    {
        base.Start();

        if (Light == null) return;

        InitialLightIntensity = Light.intensity;
        Light.enabled = false;
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

        if (IsTorch)
        {
            Torch_Update();
        }
    }

    private void Torch_Update()
    {
        if (TorchBurnAudio != null)
        {
            TorchBurnAudio.mute = FlashlightItem.isPocketed;
        }

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

    public void SwitchFlashlight(bool on)
    {
        if (TorchBurnAudio == null) return;

        if (IsTorch)
        {
            if (_playTorchBurnAudioAfterDelayCoroutine != null)
            {
                StopCoroutine(_playTorchBurnAudioAfterDelayCoroutine);
            }

            if (on)
            {
                _playTorchBurnAudioAfterDelayCoroutine = StartCoroutine(PlayTorchBurnAudioAfterDelay(0.8f));
            }
            else
            {
                TorchBurnAudio.Stop();
            }
        }
    }

    private IEnumerator PlayTorchBurnAudioAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        TorchBurnAudio.Play();
    }
}
