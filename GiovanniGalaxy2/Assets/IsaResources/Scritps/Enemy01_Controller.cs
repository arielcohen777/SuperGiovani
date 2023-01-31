using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class Enemy01_Controller : MonoBehaviour
{
    public int damage = 3;
    public const int maxHealth = 100;
    public int health;

    // sounds 
    public AudioSource enemyHits;
    public AudioSource enemyDies;

    //Enemy 
    [SerializeField] private GameObject enemy01;
    private Vector3 destination;

    Animator anim;
    private NavMeshAgent agent;

    //States 
    private bool isDead;
    private bool isAttacking;
    private bool isChasing;
    public float sightRange, attackRange, walkSpeed, chaseSpeed;

    public Transform player;

    //For Moving 
    Vector3 lastPosition;
    Transform myTransform;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemy01 = gameObject;
        health = maxHealth;
        myTransform = transform;
        lastPosition = myTransform.position;
        isChasing = false;
        isDead = false;
        isAttacking = false;

        destination = agent.destination;

        agent.radius = 0.35f;
        agent.speed = 6.0f;
        agent.avoidancePriority = 2;
        //InvokeRepeating(nameof(IsAttacking), 1f, 1f); 
    }

    private void FixedUpdate()
    {
        Follow();
        IsMoving();
        LeftBehind();
        IsAttacking();
    }

    private void IsMoving()
    {
        if (myTransform.position != lastPosition)
        {
            isChasing = true;
            if (isChasing)
            {
                anim.SetFloat("Speed", 1.0f);
            }
        }
        else
        {
            isChasing = false;
            if (!isChasing)
            {
                anim.SetFloat("Speed", 0.0f);
            }
        }
        lastPosition = myTransform.position;
    }

    private void Follow()
    {
        if (Vector3.Distance(destination, player.position) > 0.1f)
        {
            destination = player.position;
            agent.destination = destination;
        }
    }

    public bool LeftBehind()
    {
        if (Vector3.Distance(lastPosition, player.position) < 50f)
            return false;
        else
            return true;
    }

    private void IsAttacking()
    {
        if (isAttacking)
        {
            anim.SetTrigger("isAttacking");
            enemyHits.PlayOneShot(enemyHits.clip);
            agent.SetDestination(transform.position);
            transform.LookAt(player);
            Debug.Log("Estamos Atacando");
        }
    }

    public void EnemyHit()
    {
        anim.SetTrigger("isHit");
        agent.isStopped = true;
        isDead = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

}
