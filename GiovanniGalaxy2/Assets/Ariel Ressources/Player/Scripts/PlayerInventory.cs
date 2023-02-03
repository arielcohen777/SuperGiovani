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
    public void OnTriggerEnter(Collider other)
    {
        gm.crosshair.SetActive(true);
        if (other.CompareTag("Weapon"))
        {
            WeaponSO weapon = other.GetComponent<Weapon>().weapon;
            if (weapon)
            {
                inventory.AddItem(weapon);
                ui.UpdateWeaponHud();
                Destroy(other.gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        inventory.nextIdx = 0;
    }
}
