using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

public class ItemProperties
{
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

    public ItemProperties(AudioClip grabSFX, AudioClip dropSFX, AudioClip pocketSFX, AudioClip throwSFX, float verticalOffset, int floorYOffset, bool allowDroppingAheadOfPlayer, Vector3 restingRotation, Vector3 rotationOffset, Vector3 positionOffset)
    {
        GrabSFX = grabSFX;
        DropSFX = dropSFX;
        PocketSFX = pocketSFX;
        ThrowSFX = throwSFX;
        VerticalOffset = verticalOffset;
        FloorYOffset = floorYOffset;
        AllowDroppingAheadOfPlayer = allowDroppingAheadOfPlayer;
        RestingRotation = restingRotation;
        RotationOffset = rotationOffset;
        PositionOffset = positionOffset;
    }

    public ItemProperties(Item item)
    {
        GrabSFX = item.grabSFX;
        DropSFX = item.dropSFX;
        PocketSFX = item.pocketSFX;
        ThrowSFX = item.throwSFX;
        VerticalOffset = item.verticalOffset;
        FloorYOffset = item.floorYOffset;
        AllowDroppingAheadOfPlayer = item.allowDroppingAheadOfPlayer;
        RestingRotation = item.restingRotation;
        RotationOffset = item.rotationOffset;
        PositionOffset = item.positionOffset;
    }
}
