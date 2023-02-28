using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    GameManager gm; 
    public float damage;

    private void Start()
    {
        gm = GameManager.Instance; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.CompareTag("Player"))
        {
            //Debug.Log(collision.gameObject.name);
            //collision.gameObject.GetComponent<Health>().IsHit(damage);
            gm.playerHealth.IsHit(damage); 
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject, 1.5f);
        }
    }
}
