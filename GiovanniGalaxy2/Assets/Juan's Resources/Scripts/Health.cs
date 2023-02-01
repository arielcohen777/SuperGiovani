using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Health : MonoBehaviour
{
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
        armor -= damage;
        if (armor <= 0)
        {
            armor = 0;
            health -= damage;
            gm.playerIsAlive = health > 0;
        }    
    }

    public void Healing(float healingValue)
    {
        health += healingValue;
        if (health >= maxHealth)
            health = maxHealth;
    }

    public void PickArmor(float armorValue)
    {
        armor += armorValue;
        if (armor >= maxArmor)
            armor = maxArmor;
        
    }

    public void Death()
    {
        // code for what happens when player dies
        
    }
}
