using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health", menuName = "Life/Health")]

public class HealthSO : ScriptableObject
{
    public GameObject prefab;
    public string healthName;
    public float healing;
    public int price;
}
