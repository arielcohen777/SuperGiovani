using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponSO weapon;
    private void Start()
    {
        weapon.currentAmmo = weapon.magSize;
        weapon.ogMaxAmmo = weapon.maxAmmo;
    }

    private void OnApplicationQuit()
    {
        weapon.nextFire = 0;
    }
}
