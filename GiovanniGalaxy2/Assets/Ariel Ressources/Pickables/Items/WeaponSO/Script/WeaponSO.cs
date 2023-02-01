using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponSO/Weapon")]

public class WeaponSO : ScriptableObject
{
    [TextArea(1, 1)]
    public string weaponName;

    public GameObject prefab;
    public int maxAmmo;
    public int countAmmo;

    public float damage;
    public float rateOfFire;
    public float nextTimeToFire;
    public float range;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    

}
