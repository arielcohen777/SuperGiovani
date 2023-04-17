using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public LayerMask enemyLayer;
    [SerializeField] private float power;
    [SerializeField] private float radius;
    [SerializeField] private float lifespan;
    public float f = 0.1f;
    private float timeAlive;

    [SerializeField] private GameObject launchedSound;
    [SerializeField] private GameObject explosion;
    private GameObject lGO;
    private GameObject eGO;

    private bool isAlive = true;
    private bool isLaunched = true;

    [SerializeField] private ParticleSystem smokeTrailPS;
    [SerializeField] private ParticleSystem explosionPS;

    private float spawnTime;

    private Rigidbody rb;

    private GameManager gm;

    private void Start()
    {
        timeAlive = 0f;
        gm = GameManager.Instance;
        lGO = Instantiate(launchedSound);
        eGO = Instantiate(explosion);
        spawnTime = Time.time;
        smokeTrailPS.Play();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward.normalized * power, ForceMode.Impulse);
    }

    private void Update()
    {
        if (isAlive)
        {
            if (isLaunched)
            {
                Launched();
            }

            timeAlive += Time.deltaTime;
            float volume = Mathf.Pow(f, timeAlive / lifespan);
            //float volume = Mathf.Clamp(1 - (timeAlive / lifespan), 0f, 1f);

            lGO.GetComponent<AudioSource>().volume = volume;

            if (Time.time - spawnTime > lifespan)
                Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player") || other.CompareTag("Turret") || other.name.Equals("Sphere"))
            return;
        
        Explode();

    }

    private void Launched()
    {
        lGO.GetComponent<AudioSource>().Play();
        isLaunched = false;
    }

    public void Explode()
    {
        float damage = gm.playerStuff.activeWeapon.damage;

        isAlive = false;
        eGO.GetComponent<AudioSource>().Play();

        ParticleSystem expPS = Instantiate(explosionPS, transform.position, transform.rotation);

        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        Destroy(gameObject, 0.3f);

        Destroy(lGO);
        Destroy(eGO, 4f);
        Destroy(expPS, 5f);

        foreach (Collider hit in colliders)
        {
            if (!hit.CompareTag("Enemy"))
                continue;

            if (hit.GetComponentInParent<Enemy1>() != null)
                hit.GetComponentInParent<Enemy1>().IsHit(100);
            else
                hit.GetComponentInParent<Enemy2_fixed>().IsHit(100);
        }

    }
}
