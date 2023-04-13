using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemyToList : MonoBehaviour
{
    public TurretBehaviour turret;

    private void OnTriggerStay(Collider other)
    {
        if (turret.isActive)
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Found"); 
                if (!turret.enemiesInArea.Contains(other.transform.root.gameObject))
                    turret.enemiesInArea.Add(other.transform.root.gameObject);
            }
    }
}

