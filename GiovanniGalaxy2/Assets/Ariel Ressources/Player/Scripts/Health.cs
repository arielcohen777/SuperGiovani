using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Health : MonoBehaviour
{
    public bool isAlive = true;

    public float maxHealth = 100;
    public float health;

    public float maxArmor = 60;
    public float armor;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = maxArmor;
    }

    public void IsHit(float damage)
    {
        if (damage <= armor)
            armor -= damage; 
        else 
        {
            damage -= armor; 
            armor = 0; 
            health -= damage; 

        }

        gm.barUi.ArmorSlider();
        gm.barUi.HealthSlider();

        isAlive = health > 0;
    }

    public void Healing(float healingValue)
    {
        health += healingValue;
        if (health >= maxHealth)
            health = maxHealth;
        gm.barUi.HealthSlider();
    }

    public void AddArmor(float armorValue)
    {
        armor += armorValue;
        if (armor >= maxArmor)
            armor = maxArmor;
        gm.barUi.ArmorSlider();
    }

    public void Death()
    {
        // code for what happens when player dies
        
    }
}
