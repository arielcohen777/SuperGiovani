using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float damage;
    //public GameObject cloneProjectile;
    // Start is called before the first frame update
    void Start()
    {
        
     
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().IsHit(damage);
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject, 1.5f);
        }
    }
}
