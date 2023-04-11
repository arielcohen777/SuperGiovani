using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponSO/Weapon")]

public class WeaponSO : ItemSO
{ 
    [Header("Weapon Type")]
    public WeaponType wepType;
    [Header("Ammo")]
    public int maxAmmo;
    public int ogMaxAmmo;
    public int currentAmmo;
    public int magSize;
    [Header("Stats")]
    public int damage;
    public float rateOfFire;
    public float range;
    public float nextFire;
    [Header("Game Objects")]
    public GameObject gunshot;
    [Header("Recoil")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;
}

public enum WeaponType
{
    Sniper,
    Uzi,
    Bazooka,
    Shotgun,
    MachineGun
}