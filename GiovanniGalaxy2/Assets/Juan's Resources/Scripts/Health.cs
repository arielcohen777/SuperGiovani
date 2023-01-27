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
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = maxArmor;
    }

    private void Update()
    {
        //print(armor);
        //print(health);

    }

    // Update is called once per frame
    public void IsHit(float damage)
    {
        if (armor <= 0)
        {
            armor = 0;
            health -= damage;

            if (health <= 0)
            {
                Death();
            }
        }

        else
        {
            armor -= damage;
        }     

       
    }

    public void IsHealed(float healing)
    {
        health += healing;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void PickArmor(float armorVal)
    {
        armor += armorVal;

        if (armor >= maxArmor)
        {
            armor = maxArmor;
        }
    }

    public void Death()
    {
        // code for what happens when player dies
        print("Player has been killed");
    }
}
