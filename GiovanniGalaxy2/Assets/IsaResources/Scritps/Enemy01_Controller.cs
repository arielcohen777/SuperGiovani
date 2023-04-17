using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class Enemy01_Controller : MonoBehaviour
{

    // Health 
    public const int maxHealth = 100;
    public int health;

    // sounds 
    //public AudioSource enemyHits;
    //public AudioSource enemyDies;

    //Enemy 
    [SerializeField] private GameObject enemy01;
    

    Animator anim;
    private NavMeshAgent agent;

    //States 
    private bool isDead;
    private bool isAttacking;
    private bool isFollowing;
    private bool climb; 

    public Transform player;

    //Particle System Ref 
    private ParticleSystem explosionEffect;
    public int damage = 5; 

    
    private void Start()
    {
        explosionEffect = GetComponentInChildren<ParticleSystem>(); 
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemy01 = gameObject;
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            ReachedPlayer(); 
            Follow();
            IsClimbing(); 
        }
    }

    private void Follow()
    {
        if (isFollowing)
        {
            anim.SetTrigger("isFollowing");
            agent.SetDestination(player.position);
        }
    }
        
    public void ReachedPlayer()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distance <= 2.1f)
            isFollowing = false;
        else
            isFollowing = true; 
    }

    private void IsAttacking()
    {
        if (!climb)
        {
            anim.SetTrigger("isAttacking");
            //enemyHits.PlayOneShot(enemyHits.clip);
            health -= damage; 
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            isFollowing = false;
        }
        else
            isFollowing = true;
        /*
        if (isAttacking && !climb)
        {
            anim.SetTrigger("isAttacking");
            //enemyHits.PlayOneShot(enemyHits.clip); 
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            isFollowing = false; 
        }
        else
            isFollowing = true; 
        //Debug.Log("We are attacking");
        */
    }


    private void IsClimbing()
    {
        if (climb)
        {
            anim.SetBool("isClimbing", true);
            //isFollowing = false;
            //agent.SetDestination(player.position); 
            //Debug.Log("funciona el climb");
        }
        else if(!climb)
            anim.SetBool("isClimbing", false); 
    }

    public void IsHit(int damage)
    {
        health -= damage;
        anim.SetTrigger("isHit");
        anim.SetInteger("hitIndex", Random.Range(0, 2));

        if (health <= 0)
            Death(); 
    }

    private void Death()
    {
        agent.isStopped = true;
        isDead = true;
        anim.SetTrigger("isDead");
        anim.SetInteger("deadIndex", Random.Range(0, 2));
        explosionEffect.Play();
        Destroy(enemy01, 4f); 
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
            IsAttacking();
        }
        if (other.CompareTag("Wall"))
        {
            climb = true; 
            IsClimbing();
            Debug.Log("se quedo en la pared");
        }
    }
   */
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            climb = true;
            anim.SetBool("isClimbing", true); 
            //IsClimbing();
            Debug.Log("entro a la pared");
        }
        if (other.CompareTag("Player"))
        {
            // isAttacking = true;
            IsAttacking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // isAttacking = false;
            isFollowing = true;
        }
        if (other.CompareTag("Wall"))
        {
            climb = false;
            //IsClimbing(); 
        }

    }

}
