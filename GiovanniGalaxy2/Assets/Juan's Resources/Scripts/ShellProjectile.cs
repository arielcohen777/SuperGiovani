using System;
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

    private ParticleSystem smokeTrail;
    Rigidbody rb;
  
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        smokeTrail = GetComponentInChildren<ParticleSystem>();
        smokeTrail.Play();
    }
    private void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {      
        {
            Enemy1 enemy = collision.collider.GetComponentInParent<Enemy1>();
            Enemy2_fixed enemy2 = collision.collider.GetComponentInParent<Enemy2_fixed>();
            //Enemy2 enemy2 = collision.collider.GetComponentInParent<Enemy2>();

            if (collision.collider.CompareTag("Enemy"))
            {
                if (enemy != null) enemy.IsHit(damageEnemy);
                else enemy2.IsHit(damageEnemy);

                rb.AddForce(gameObject.transform.forward * 30f, ForceMode.Impulse);
                Debug.Log("we bouncing against enemies!");
            }

            else if (collision.collider.CompareTag("Player"))
            {
                collision.gameObject.GetComponentInParent<Health>().IsHit(damagePlayer);
                Destroy(gameObject);
            }

            Destroy(gameObject, 15f);
        }
    }   
}

