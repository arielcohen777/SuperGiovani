using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponSO/Weapon")]

public class WeaponSO : ItemSO
{
    [Header("Ammo")]
    public int maxAmmo;
    public int currentAmmo;
    public int magSize;
    public int ogMaxAmmo;
    [Header("Stats")]
    public float damage;
    public float rateOfFire;
    public float range;
    public float nextFire;
    [Header("Game Objects")]
    public AudioSource gunshot;
}
