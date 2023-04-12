using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 10f;
    public float damage = 20f;
    public float fireRate = 1f;
    public Transform firePoint;

    private float fireCountdown = 0f;

    void Update()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, range);

        foreach (Collider enemy in hitEnemies)
        {
            // check if the enemy has an Enemy2_fixed script attached to it
            Enemy2_fixed enemyScript = enemy.GetComponent<Enemy2_fixed>();
            if (enemyScript != null && enemyScript.health > 0)
            {
                // reduce enemy health and destroy it if health reaches 0
                enemyScript.health -= damage;
                if (enemyScript.health <= 0)
                {
                    enemyScript.Death();
                }
            }
        }
    }
}
