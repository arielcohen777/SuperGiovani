using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [Header("Name of item")]
    [TextArea(1, 1)]
    public string itemName;
    [Header("Price of item")]
    public int price;
    [Header("Prefab of item")]
    public GameObject prefab;
    [Header("Type of item")]
    public ItemType type;
    [Header("ONLY FOR LEVER! Time of use")]
    public int timer;
}

public enum ItemType
{
    Weapon,
    Armor,
    Health,
    Battery,
    Lever
}
