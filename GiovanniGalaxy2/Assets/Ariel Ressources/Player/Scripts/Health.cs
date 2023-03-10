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
        gm = GameManager.Instance;
        health = maxHealth;
        armor = maxArmor;
    }

    public void IsHit(float damage)
    {
        //If the damage done is less than the amount of armor, reduce armor
        if (damage <= armor)
            armor -= damage;
        //If not, reduce damage to how much damage done to armor, set
        //armor to 0 and lower health by remaining damage
        else
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }

        //Update health and armor sliders
        gm.barUi.ArmorSlider();
        gm.barUi.HealthSlider();

        //Set isAlive to false if no more health.
        if (isAlive = health > 0)
            Death();
    }

    public void AddHealth(float healingValue)
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
