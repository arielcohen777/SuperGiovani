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
            gm.activeWeapon = GetNextWeapon();
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
        {
            Container.Add(toAdd);
        }
        else
        {
            Container[idx].AddAmmo(wep.maxAmmo);
        }
    }

    public void ChooseWeapon(int idx)
    {
        GameManager gm = GameManager.Instance;
        gm.activeWeapon = Container[idx];
    }

    public WeaponSlot GetNextWeapon()
    {
        if (Container.Count == 0)
            return null;
        WeaponSlot wepToGive = Container[nextIdx];
        if (Container.Count == 1)
            return wepToGive;
        nextIdx++;
        if (nextIdx == Container.Count)
            nextIdx = 0;
        return wepToGive;
    }

}

[System.Serializable]
public class WeaponSlot
{
    public WeaponSO weapon;
    public int ammoCount;
    public int ammoMax;
    public float nextFire;
    public string weaponName;

    public WeaponSlot(WeaponSO wep)
    {
        weapon = wep;
        ammoCount = wep.countAmmo;
        ammoMax = wep.maxAmmo;
        weaponName = wep.name;
    }

    public void AddAmmo(int value)
    {
        ammoCount += value;
        if (ammoCount > ammoMax)
            ammoCount = ammoMax;
    }

    public bool Equals(WeaponSO obj)
    {
        return obj.name.Equals(weaponName);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
