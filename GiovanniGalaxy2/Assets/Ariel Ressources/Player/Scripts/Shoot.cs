using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    private static GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public static void Shooting(WeaponSO weapon)
    {
        //Muzzle Flash
        if (weapon.muzzleFlash != null) 
            weapon.muzzleFlash.Play();

        //Aiming
        if (Physics.Raycast(gm.cam.transform.position, gm.cam.transform.forward, out RaycastHit hit, weapon.range))
        {
            //Making sure it's an enemy
            Enemy2 target = hit.transform.GetComponent<Enemy2>();
            if (target != null)
            {
                target.IsHit((int)weapon.damage);
            }

            //Target impact effect
            if (weapon.impactEffect != null)
            {
                GameObject impactGO = Instantiate(weapon.impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }

    }
}
