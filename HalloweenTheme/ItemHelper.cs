using com.github.zehsteam.HalloweenTheme.Data;

namespace com.github.zehsteam.HalloweenTheme;

internal static class ItemHelper
{
    public static Item GetItemByName(string itemName)
    {
        foreach (var item in StartOfRound.Instance.allItemsList.itemsList)
        {
            if (item.itemName.Equals(itemName))
            {
                return item;
            }
        }

        return null;
    }
    
    public static void SetItemProperties(string itemName, ItemProperties itemProperties)
    {
        Item item = GetItemByName(itemName);

        if (item == null)
        {
            Plugin.Logger.LogError($"Failed to set item properties for \"{itemName}\". Could not find item.");
            return;
        }

        SetItemProperties(item, itemProperties);
    }

    public static void SetItemProperties(Item item, ItemProperties itemProperties)
    {
        if (item == null)
        {
            Plugin.Logger.LogError($"Failed to set item properties. Item is null.");
            return;
        }

        if (itemProperties == null)
        {
            Plugin.Logger.LogError($"Failed to set item properties for \"{item.itemName}\". ItemProperties is null.");
            return;
        }

        if (itemProperties.GrabSFX != null)
        {
            item.grabSFX = itemProperties.GrabSFX;
        }

        if (itemProperties.DropSFX != null)
        {
            item.dropSFX = itemProperties.DropSFX;
        }

        if (itemProperties.PocketSFX != null)
        {
            item.pocketSFX = itemProperties.PocketSFX;
        }

        if (itemProperties.ThrowSFX != null)
        {
            item.throwSFX = itemProperties.ThrowSFX;
        }

        item.verticalOffset = itemProperties.VerticalOffset;
        item.floorYOffset = itemProperties.FloorYOffset;
        item.allowDroppingAheadOfPlayer = itemProperties.AllowDroppingAheadOfPlayer;
        item.restingRotation = itemProperties.RestingRotation;
        item.rotationOffset = itemProperties.RotationOffset;
        item.positionOffset = itemProperties.PositionOffset;

        Plugin.Instance.LogInfoExtended($"Set item properties for \"{item.itemName}\".");
    }
}
