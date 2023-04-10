using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemyToList : MonoBehaviour
{
    public TurretBehaviour turret;

    private void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = turret.radius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (turret.isActive)

            if (other.CompareTag("Enemy"))
            {
                Enemy2_fixed enemy2 = other.gameObject.transform.root.GetComponent<Enemy2_fixed>();
                Enemy1 enemy1 = other.gameObject.transform.root.GetComponent<Enemy1>();

                if (enemy1 != null)
                    if (!turret.enemiesInArea.Contains(other.transform.root.gameObject))
                        turret.enemiesInArea.Add(other.transform.root.gameObject);

                if (enemy2 != null)
                    if (!turret.enemiesInArea.Contains(other.transform.root.gameObject))
                        turret.enemiesInArea.Add(other.transform.root.gameObject);
            }
    }
}

