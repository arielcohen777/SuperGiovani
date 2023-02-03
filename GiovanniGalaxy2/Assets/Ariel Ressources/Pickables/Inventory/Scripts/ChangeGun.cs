using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    GameManager gm;
    GameObject currentWeapon;
    bool canChange = true;
    void Start()
    {
        gm = GameManager.Instance;
    }

    public void SwitchWeapons()
    {
        if (canChange && gm.inventory.Container.Count > 1)
        {
            Debug.Log("Switching");
            canChange = false;
            gm.activeWeapon = gm.inventory.GetNextWeapon();
            gm.wepUi.UpdateWeaponHud();
            gm.shoot.anim.SetBool("Switch", true);
            Invoke("UpdateGunMesh", 0.30f);
            StartCoroutine(CanChangeAgain());
        }
    }

    public void UpdateGunMesh()
    {
        Debug.Log("Updating");
        gm.shoot.anim.SetBool("Switch", false);
        currentWeapon = gm.activeWeapon.weapon.prefab;
        GetComponent<MeshFilter>().sharedMesh = currentWeapon.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<Renderer>().sharedMaterial = currentWeapon.GetComponent<Renderer>().sharedMaterial;    }

    IEnumerator CanChangeAgain()
    {
        yield return new WaitForSeconds(0.2f);
        canChange = true;
    }
}
