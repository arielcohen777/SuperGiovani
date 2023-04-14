using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detach : MonoBehaviour
{
    private Transform myTransform;
    private Enemy1 enemy;
    private Enemy2_fixed enemy2;



    void Start()
    {
        enemy = GetComponentInParent<Enemy1>();
        enemy2 = GetComponentInParent<Enemy2_fixed>();
        myTransform = gameObject.transform;
    }

    private void Update()
    {
        if (enemy != null)
        {
            if (enemy.isDead)
            {
                myTransform.SetParent(null);
                Destroy(gameObject, 2f);
            }
        }
        if (enemy2 != null)
        {
            if (enemy2.isDead)
            {
                myTransform.SetParent(null);
                Destroy(gameObject, 2f);
            }
        }
        
    }
    
}
