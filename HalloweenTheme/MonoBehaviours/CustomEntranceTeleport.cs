using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.MonoBehaviours;

public class CustomEntranceTeleport : MonoBehaviour
{
    public bool IsEntranceToBuilding = true;
    public int EntranceId;
    public Transform EntrancePoint;

    private void Start()
    {
        EntranceTeleport entranceTeleport = LevelHelper.GetEntranceTeleport(EntranceId, IsEntranceToBuilding);

        if (entranceTeleport == null)
        {
            Plugin.Logger.LogError($"Failed to set custom entrance teleport. EntranceTeleport is null. (EntranceId: {EntranceId}, IsEntranceToBuilding: {IsEntranceToBuilding})");
            return;
        }

        entranceTeleport.transform.SetPositionAndRotation(transform.position, transform.rotation);
        entranceTeleport.transform.localScale = transform.localScale;
        entranceTeleport.entrancePoint.SetPositionAndRotation(EntrancePoint.position, EntrancePoint.rotation);

        Utils.HideGameObject(gameObject);

        Plugin.Instance.LogInfoExtended($"Successfully set custom entrance teleport. (EntranceId: {EntranceId}, IsEntranceToBuilding: {IsEntranceToBuilding})");
    }
}
