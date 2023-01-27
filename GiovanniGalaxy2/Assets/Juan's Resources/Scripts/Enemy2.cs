using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class Enemy2 : MonoBehaviour
{

    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attack
     
    public GameObject projectile;
    public GameObject spawnPoint;
    public GameObject cloneProjectile;

    //flee
    public Vector3 direction, distanceRunTo, runTo;
    public bool isRunning;

    //health system     

    public const int maxHealth = 100;
    public int health;

    private GameObject enemy2;
    public bool isDead;    


    //states

    public float sightRange, attackRange, fleeRange;
    public float walkSpeed, chasingSpeed, fleeingSpeed;
    public bool playerInSightRange, playerInAttackRange, playerInFleeRange;

    // anim
    public Animator anim;    


    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        enemy2 = gameObject;
        health = maxHealth;         
    }

    private void Update()
    {

        // check sight, range, running 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange +0.5f, whatIsPlayer);
        playerInFleeRange = Physics.CheckSphere(transform.position, fleeRange, whatIsPlayer);

        CheckIsRunningAway();        


        if (!playerInSightRange && !playerInAttackRange && !playerInFleeRange && !isRunning && !isDead) Patrol();
        if (playerInSightRange && !playerInAttackRange && !playerInFleeRange && !isRunning && !isDead) Chase();
        if (playerInSightRange && playerInAttackRange && !playerInFleeRange && !isRunning && !isDead) Attack();
        if (playerInFleeRange && !isDead) RunAway();

        
       
        //print(distanceRunTo.magnitude);
        //print(runTo);
        
    }
    private void Patrol()
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            enemy.SetDestination(walkPoint);

        anim.SetTrigger("patrolling");

        enemy.speed = walkSpeed;

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;         
    }
    private void Chase()
    {
        anim.SetTrigger("chasing");
        enemy.speed = chasingSpeed;
        enemy.SetDestination(player.position);
    }

    public void AttackAnimationEvent()
    {
        anim.SetTrigger("attacking");
        cloneProjectile =  Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
        Rigidbody rb = cloneProjectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.forward * 8f, ForceMode.Impulse);
       // Destroy(cloneProjectile, 1.5f);
    }
    public void Attack()
    {
        //stop agent movement
        transform.LookAt(player);
        enemy.SetDestination(transform.position);
        anim.SetTrigger("attacking");      
        
    }

    private void RunAway()
    {
        anim.SetTrigger("fleeing");
        enemy.speed = fleeingSpeed;
        isRunning = true;
        direction = (transform.position - player.transform.position).normalized;
        runTo = transform.position + ( direction * 9f);
        enemy.SetDestination(runTo);  
    }

    private void CheckIsRunningAway()
    {
        distanceRunTo = transform.position - runTo;

        if (distanceRunTo.magnitude < 2.5f)
        {
            isRunning = false;
        }
    }

    public void IsHit(int damage)
    {
      
        health -= damage;
        anim.SetTrigger("isHit");
        anim.SetInteger("hitIndex", Random.Range(0, 2));        
  
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        enemy.isStopped = true;
        isDead = true;
        anim.SetTrigger("isDead");
        anim.SetInteger("deadIndex", Random.Range(0, 2));
        Destroy(enemy2, 4f);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fleeRange);

        Gizmos.color = Color.black;
        //Gizmos.DrawLine(transform.position,transform.position + direction);
        Gizmos.DrawLine(transform.position, player.transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, runTo);

    }
}
