using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public LayerMask enemyLayer;
    [SerializeField] private float power = 10f;
    [SerializeField] private float radius = 7f;
    [SerializeField] private float lifespan = 3f;

    private bool isAlive = true;

    [SerializeField] private ParticleSystem smokeTrailPS;
    [SerializeField] private ParticleSystem explosionPS;

    private float spawnTime;

    private Rigidbody rb;

    [System.Obsolete]
    private void Start()
    {
        spawnTime = Time.time;
        smokeTrailPS.Play();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward.normalized * power, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (!isAlive)
            return;

        if (Time.time - spawnTime > lifespan)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.name.Equals("Player"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        isAlive = false;

        Instantiate(explosionPS, transform.position, transform.rotation);
      
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        Destroy(gameObject, 0.3f);

        foreach (Collider hit in colliders)
        {
            if (!hit.CompareTag("Enemy"))
                continue;

            if (hit.GetComponentInParent<Enemy1>() != null)
                hit.GetComponentInParent<Enemy1>().IsHit(100);
            else
                hit.GetComponentInParent<Enemy2>().IsHit(100);
        }

    }
}
