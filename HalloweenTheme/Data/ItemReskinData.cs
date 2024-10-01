using System;
using UnityEngine;

namespace com.github.zehsteam.HalloweenTheme.Data;

[Serializable]
public class ItemReskinData
{
    public string ItemName;
    public GameObject Prefab;

    public ItemReskinData(string itemName, GameObject prefab)
    {
        ItemName = itemName;
        Prefab = prefab;
    }
}
