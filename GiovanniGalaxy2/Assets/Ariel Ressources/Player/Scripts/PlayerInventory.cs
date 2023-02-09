using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    GameManager gm;
    WeaponUI ui;
    private void Start()
    {
        gm = GameManager.Instance;
        ui = gm.wepUi;
    }

    public void BuyItem(GameObject item)
    {
        gm.crosshair.SetActive(true);
        if (item.CompareTag("Weapon"))
        {
            WeaponSO weapon = item.GetComponent<Weapon>().weapon;
            inventory.AddItem(weapon);
            ui.UpdateWeaponHud();
            gm.coins -= weapon.price;
        }

        if (item.CompareTag("Health"))
        {
            HealthSO health = item.GetComponent<HealthItem>().healthSO;
            gm.player.GetComponent<Health>().Healing(health.healing);
            gm.coins -= health.price;
        }

        if (item.CompareTag("Armor"))        
        {
            ArmorSO armor = item.GetComponent<ArmorItem>().armorSO;
            gm.player.GetComponent<Health>().AddArmor(armor.amount);
            gm.coins -= armor.price;
        }

        Destroy(item.gameObject);

        gm.UpdateCoinDisplay();
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        inventory.nextIdx = 0;
    }
}
