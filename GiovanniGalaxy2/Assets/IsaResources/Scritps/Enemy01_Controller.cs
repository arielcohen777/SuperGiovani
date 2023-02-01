using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class Enemy01_Controller : MonoBehaviour
{
    //public int damage = 3;
    //public const int maxHealth = 100;
    //public int health;

    // sounds 
    //public AudioSource enemyHits;
    //public AudioSource enemyDies;

    //Enemy 
    [SerializeField] private GameObject enemy01;
   // private Vector3 destination;

    Animator anim;
    private NavMeshAgent agent;

    //States 
    private bool isDead;
    public bool isAttacking;
    private bool isFollowing;
   // public float sightRange, attackRange, walkSpeed, chaseSpeed;

    public Transform player;

    //For Moving 
    //Vector3 lastPosition;
    //Transform myTransform;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemy01 = gameObject;
        //health = maxHealth;
       // myTransform = transform;
       // lastPosition = myTransform.position;        
     

      //  destination = agent.destination;

        //agent.radius = 0.35f;
        //agent.speed = 6.0f;
        //agent.avoidancePriority = 2;
        //InvokeRepeating(nameof(IsAttacking), 1f, 1f); 
    }

    private void FixedUpdate()
    {

        
       ReachedPlayer();
       Follow();
        
      //  IsMoving();
        //LeftBehind();
        //IsAttacking();
    }

    //private void IsMoving()
    //{
    //    if (myTransform.position != lastPosition)
    //    {
    //        isChasing = true;
    //        if (isChasing)
    //        {
    //            anim.SetFloat("Speed", 1.0f);
    //        }
    //    }
    //    else
    //    {
    //        isChasing = false;
    //        if (!isChasing)
    //        {
    //            anim.SetFloat("Speed", 0.0f);
    //        }
    //    }
    //    lastPosition = myTransform.position;
    //}

    private void Follow()
    {

        if (isFollowing)
        {
            anim.SetTrigger("isFollowing");
            agent.SetDestination(player.position);
        }
     
    }

    private void ReachedPlayer()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //print(distance);

        if (distance <= 2.1f)
        {
            isFollowing = false;
        }

        else
        {
            isFollowing = true;           
        }

        //print(isFollowing);
    }

    //public bool LeftBehind()
    //{
    //    if (Vector3.Distance(lastPosition, player.position) < 50f)
    //        return false;
    //    else
    //        return true;
    //}

    private void IsAttacking()
    {              
            anim.SetTrigger("isAttacking");
           // enemyHits.PlayOneShot(enemyHits.clip);
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            Debug.Log("Estamos Atacando");
            isFollowing = false;
    }

    //public void EnemyHit()
    //{
    //    if (isAttacking)
    //    {
    //        anim.SetTrigger("isHit");
    //        agent.isStopped = true;
    //        print("we are attacking");
    //    }
 
      
        
   // }   

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAttacking();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsAttacking();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }
}
