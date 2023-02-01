using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellProjectile : MonoBehaviour
{
    public float damageEnemy = 100;
    public float damagePlayer = 40;

    [SerializeField] private float speed;
    [SerializeField] private Vector3 rotation;


    private void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().IsHit(damagePlayer);
            Destroy(gameObject);
        }     

        else
        {
            Destroy(gameObject, 20f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dummie"))
        {
            other.gameObject.GetComponent<Enemy2>().IsHit(damageEnemy);            
        }
    }
}
