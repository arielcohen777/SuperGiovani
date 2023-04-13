using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<WeaponSlot> Container = new List<WeaponSlot>();

    public int nextIdx = 0;

    public void AddItem(WeaponSO wep)
    {
        WeaponSlot toAdd = new WeaponSlot(wep);
        bool hasItem = false;
        int idx = 0;
        GameManager gm = GameManager.Instance;

        if (Container.Count == 0)
        {
            Container.Add(toAdd);
            gm.playerStuff.activeWeapon = GetNextWeapon();
            if (toAdd.gunshot != null)
            gm.shoot.gunshot.clip = toAdd.gunshot.GetComponent<AudioSource>().clip;
            gm.wepUi.UpdateWeaponHud();
            gm.changeGun.UpdateGunMesh();
            return;
        }

        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].Equals(wep))
            {
                hasItem = true;
                idx = i;
                break;
            }
        }

        if(!hasItem)
            Container.Add(toAdd);
        else
            Container[idx].AddAmmo(wep);
        
    }

    public WeaponSlot GetNextWeapon()
    {
        if (Container.Count == 0)
           return null;
        
        if (Container.Count == 1)
            return Container[0];

        nextIdx++;
        if (nextIdx == Container.Count)
            nextIdx = 0;
        return Container[nextIdx];

        
    }

}

[System.Serializable]
public class WeaponSlot
{
    public WeaponSO weaponSo;
    public float damage;
    public float range;
    public int currentAmmo;
    public int maxAmmo;
    public int magSize;
    public float nextFire;
    public string weaponName;
    public float rateOfFire;
    public int ogMaxAmmo;
    public GameObject gunshot;
    public WeaponType wepType;

    public WeaponSlot(WeaponSO wep)
    {
        weaponSo = wep;
        currentAmmo = wep.currentAmmo;
        magSize = wep.magSize;
        maxAmmo = wep.maxAmmo;
        ogMaxAmmo = wep.maxAmmo;
        weaponName = wep.name;
        gunshot = wep.gunshot;
        damage = wep.damage;
        range = wep.range;
        rateOfFire = wep.rateOfFire;
        wepType = wep.wepType;
    }

    public void AddAmmo(WeaponSO wep)
    {
        maxAmmo = wep.ogMaxAmmo;
    }
}
