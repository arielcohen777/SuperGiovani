using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    string toReturn;

    private void Start()
    {
        if (gameObject.CompareTag("Weapon"))
        {
            WeaponSO wep = GetComponent<Weapon>().weapon;
            toReturn = "Buy " + wep.weaponName + "\n" + wep.price + " Coins";
        }
        else if (gameObject.CompareTag("Health"))
        {
            HealthSO health = GetComponent<HealthItem>().healthSO;
            toReturn = "Buy " + health.healthName + "\n" + health.price + " Coins";
        }
        else
        {
            ArmorSO armor = GetComponent<ArmorItem>().armorSO;
            toReturn = "Buy " + armor.armorName + "\n" + armor.price + " Coins";

        }
    }

    public string ShowPrompt()
    {
        return toReturn;
    }

}