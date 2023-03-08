using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private ParticleSystem energySparks;
    GameManager gm; 
    public float damage;

    private void Start()
    {
        gm = GameManager.Instance; 
        energySparks = GetComponentInChildren<ParticleSystem>();
        energySparks.Play();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider.CompareTag("Player"))
        {           
            gm.playerHealth.IsHit(damage); 
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject, 1.5f);
        }
    }
}
