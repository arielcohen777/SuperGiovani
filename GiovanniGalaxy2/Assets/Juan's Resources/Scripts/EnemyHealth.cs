using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    private Enemy2 enemy2;
    private Enemy1 enemy1;

    // Start is called before the first frame update
    void Start()
    {
        enemy2 = gameObject.GetComponent<Enemy2>();
        health = maxHealth;
    }

    private void Update()
    {
   
        //print(health);

    }

    // Update is called once per frame
    public void IsHit(float damage)
    {   
            health -= damage;

            if (health <= 0)
            {
                 
            }        
    }  
     
}
