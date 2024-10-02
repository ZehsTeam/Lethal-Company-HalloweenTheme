using GameNetcodeStuff;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class SmartLight : MonoBehaviour
{
    public Light Light;
    public bool DisableAfterMaxDistance = true;
    public float MaxDistance = 60f;

    private void Update()
    {
        if (Light == null) return;
        if (!DisableAfterMaxDistance) return;

        PlayerControllerB playerScript = PlayerUtils.GetLocalPlayerScript();
        if (playerScript == null) return;

        Light.enabled = Vector3.Distance(transform.position, playerScript.transform.position) <= MaxDistance;
    }
}
