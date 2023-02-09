using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Life/Armor")]

public class ArmorSO : ScriptableObject
{
    public GameObject prefab;
    public string armorName;
    public float amount;
    public int price;
}
