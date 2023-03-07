using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
            collision.gameObject.GetComponentInParent<Health>().IsHit(damagePlayer);
            Destroy(gameObject);
        }     

        else
        {
            Destroy(gameObject, 20f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy1 enemy = other.GetComponentInParent<Enemy1>();
        Enemy2 enemy2 = other.GetComponentInParent<Enemy2>();

        if (other.CompareTag("Enemy"))
        {
            if (enemy != null) enemy.IsHit(damageEnemy);
            else enemy2.IsHit(damageEnemy);
                       
        }
    }
}
