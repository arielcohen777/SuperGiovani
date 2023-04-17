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
        if (item.type.Equals(ItemType.Health))
        {
            HealthSO health = (HealthSO) item;
            if (gm.player.GetComponent<Health>().health != gm.player.GetComponent<Health>().maxHealth)
                gm.player.GetComponent<Health>().AddHealth(health.amount);
            else return;
        }

        //Armor
        else if (item.type.Equals(ItemType.Armor))
        {
            ArmorSO armor = (ArmorSO) item;
            if (gm.player.GetComponent<Health>().armor != gm.player.GetComponent<Health>().maxArmor)
                gm.player.GetComponent<Health>().AddArmor(armor.amount);
            else return;

        }

        //Destroy item and update coin display
        gm.playerStuff.coins -= item.price;
        Destroy(objectToBuy);
        gm.playerStuff.UpdateCoinDisplay();
    }

    private void OnApplicationQuit()
    {
        foreach(WeaponSlot w in gm.inventory.Container)
        {
            w.weaponSo.nextFire = 0;
        }
        inventory.Container.Clear();
        inventory.nextIdx = 0;
    }
}
