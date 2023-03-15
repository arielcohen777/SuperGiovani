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
            canChange = false;
            gm.playerStuff.activeWeapon = gm.inventory.GetNextWeapon();
            gm.wepUi.UpdateWeaponHud();
            gm.shoot.anim.SetBool("Switch", true);
            Invoke("UpdateGunMesh", 0.30f);
            StartCoroutine(CanChangeAgain());
        }
    }
    
    //Updates the mesh
    /*public void UpdateGunMesh()
    {
        gm.shoot.anim.SetBool("Switch", false);
        currentWeapon = gm.playerStuff.activeWeapon.weapon.prefab;
        GetComponent<MeshFilter>().sharedMesh = currentWeapon.GetComponent<MeshFilter>().sharedMesh;
        GetComponent<Renderer>().sharedMaterial = currentWeapon.GetComponent<Renderer>().sharedMaterial;    
    }*/

    //Adds weapon as a child
    public void UpdateGunMesh()
    {
        gm.shoot.anim.SetBool("Switch", false);
        currentWeapon = gm.playerStuff.activeWeapon.weapon.prefab;

        if (transform.childCount > 0)
            Object.Destroy(gameObject.transform.GetChild(0).gameObject);

        GameObject newGun = Instantiate(currentWeapon, gameObject.transform);
        newGun.transform.parent = gameObject.transform;
    }

    IEnumerator CanChangeAgain()
    {
        yield return new WaitForSeconds(0.2f);
        canChange = true;
    }
}
