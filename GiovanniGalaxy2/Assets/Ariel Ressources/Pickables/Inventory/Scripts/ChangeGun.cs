using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    GameManager gm;
    GameObject currentWeapon;
    MeshFilter meshGun;
    bool canChange = true;
    void Start()
    {
        gm = GameManager.Instance;
    }

    public void UpdateGunMesh()
    {
        currentWeapon = gm.activeWeapon.weapon.prefab;
        meshGun = currentWeapon.GetComponent<MeshFilter>();
        GetComponent<MeshFilter>().sharedMesh = meshGun.sharedMesh;
    }

    public void SwitchWeapons()
    {
        if (canChange)
        {
            canChange = false;
            gm.activeWeapon = gm.inventory.GetNextWeapon();
            gm.wepUi.UpdateWeaponHud();
            UpdateGunMesh();
            StartCoroutine(CanChangeAgain());
        }
    }

    IEnumerator CanChangeAgain()
    {
        yield return new WaitForSeconds(0.2f);
        canChange = true;
    }
}
