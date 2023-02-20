using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void BuyItem(GameObject objectToBuy)
    {
        ItemSO item = objectToBuy.GetComponent<Item>().item;
        //If not enough money, don't buy
        if (gm.playerStuff.coins < item.price) return;

        //Weapon
        if (item.type.Equals(ItemType.Weapon))
        {
            gm.inventory.AddItem((WeaponSO) item);
            if (inventory.Container.Count != 0)
                gm.crosshair.SetActive(true);
        }
        //Health
        else if (item.type.Equals("Health"))
        {
            HealthSO health = (HealthSO) item;
            Health pHealth = gm.player.GetComponent<Health>();
            if (pHealth.health != pHealth.maxHealth)
                pHealth.AddHealth(health.amount);
            else return;
        }
        //Armor
        else if (item.type.Equals("Armor"))
        {
            ArmorSO armor = (ArmorSO) item;
            Health pHealth = gm.player.GetComponent<Health>();
            if (pHealth.armor != pHealth.maxArmor)
                pHealth.AddArmor(armor.amount);
            else return;

        }

        //Destroy item and update coin display
        gm.playerStuff.coins -= item.price;
        Destroy(objectToBuy);
        gm.playerStuff.UpdateCoinDisplay();
    }

    private void OnApplicationQuit()
    {
        if (!gm.playerStuff.activeWeapon.weaponName.Equals(string.Empty))
            gm.playerStuff.activeWeapon.weapon.nextFire = 0;
        inventory.Container.Clear();
        inventory.nextIdx = 0;
    }
}
