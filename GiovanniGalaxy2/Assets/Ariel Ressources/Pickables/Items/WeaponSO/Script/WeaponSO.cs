using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponSO/Weapon")]

public class WeaponSO : ScriptableObject
{
    [Header("Name of Gun")]
    [TextArea(1, 1)]
    public string weaponName;

    [Header("Gun Prefab (Must have texture)")]
    public GameObject prefab;
    [Header("Ammo")]
    public int maxAmmo;
    public int currentAmmo;
    public int magSize;
    [Header("Stats")]
    public float damage;
    public float rateOfFire;
    public float nextTimeToFire;
    public float range;
    [Header("Game Objects")]
    public GameObject impactEffect;
    public AudioSource gunshot;    

}
