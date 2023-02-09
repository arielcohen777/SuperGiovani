using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Enemy2 : MonoBehaviour
{

    //SAVS - Particle System ref 
    private ParticleSystem explosionEffect;

    [Header("Fov")]
    public float viewAngle;

    [Header("Checks")]
    [SerializeField] private bool playerInSightRange;
    [SerializeField] private bool playerInAttackRange;
    [SerializeField] private bool playerInFleeRange;
    public bool playerSpotted;
    public bool enemyAlerted;

    //
    [Header("Navigation")]
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsObstruction;

    // patrol
    [Header("Patrol")]
    public float walkPointRange;
    private Vector3 walkPoint;
    private bool walkPointSet;


    //attack
    [Header("Enemy Projectile")]
    public GameObject projectile;
    public GameObject spawnPoint;
    private GameObject cloneProjectile;

    //flee
    private Vector3 direction, distanceRunTo, runTo;
    private bool isRunning;

    //health system     

    [Header("Set Enemy Health Stats")]
    public const float maxHealth = 100;
    public float health;

    private GameObject enemy2;
    public bool isDead;

    //states
    [Header("Set State Ranges")]
    public float sightRange;
    public float attackRange;
    public float fleeRange;

    [Header("Set Speeds")]
    public float walkSpeed;
    public float chasingSpeed;
    public float fleeingSpeed;

    // anim
    private Animator anim;

    private void Start()
    {
        explosionEffect = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        enemy2 = gameObject;
        health = maxHealth;
    }

    private void Update()
    {
        CheckRanges();
    }

    private void FixedUpdate()
    {
        CheckIsRunningAway();
        CheckPlayerSpotted();
        ChechStates();
    }

    private void ChechStates()
    {
        if (!isDead)
        {
            if (!isRunning)
            {
                if (!playerInFleeRange)
                {
                    if (!enemyAlerted && !playerInAttackRange) Patrol();
                    if ((playerSpotted && !playerInAttackRange) || (enemyAlerted && !playerInAttackRange)) Chase();
                    if (playerSpotted && playerInAttackRange) Attack();
                }
            }
            if (playerInFleeRange) RunAway();
        }
    }

    private void CheckRanges()
    {
        // check sight, attack , flee ranges 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInFleeRange = Physics.CheckSphere(transform.position, fleeRange, whatIsPlayer);
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
        {
            StartCoroutine(Idle());
            walkPointSet = false;
        }
    }
    private IEnumerator Idle()
    {
        anim.SetTrigger("Idle");
        enemy.isStopped = true;
        yield return new WaitForSeconds(3);
        transform.LookAt(walkPoint);
        enemy.isStopped = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // is better to use this instead of raycast to find a random point to avoid sending the agent to a destination without navmesh.  

        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 1f, NavMesh.AllAreas))
        {
            walkPoint = navMeshHit.position;
            walkPointSet = true;
        }
    }
    private void Chase()
    {
        anim.SetTrigger("chasing");
        enemy.speed = chasingSpeed;
        enemy.SetDestination(player.position);
        SmoothRotation(0.15f, 1);
    }   

    private void CheckPlayerSpotted()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        if (rangeCheck.Length != 0)
        {
            Vector3 directionTarget = (player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, directionTarget, distanceToTarget, whatIsObstruction))
                {
                    Debug.DrawRay(transform.position, directionTarget * sightRange, Color.blue);
                    playerSpotted = true;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, directionTarget * sightRange, Color.red);
                playerSpotted = false;
            }
        }
        else if (playerSpotted)
        {
            playerSpotted = false;
        }
    }

    public void AttackAnimationEvent()
    {
        cloneProjectile = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
        Rigidbody rb = cloneProjectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse); 
    }
    public void Attack()
    {
       
        SmoothRotation(0.5f, 1);
        float distanceToTarget = Vector3.Distance(transform.position, player.position);
        if (!Physics.Raycast(transform.position, direction, distanceToTarget, whatIsObstruction))
        {
            enemy.SetDestination(transform.position);
            anim.SetTrigger("attacking");
        }
        else
        {
            Chase();
        }
    }
    private void RunAway()
    {                   
        isRunning = true;            
        anim.SetTrigger("fleeing");            
        enemy.speed = fleeingSpeed;         
        SmoothRotation(0.2f, -1);
        runTo = transform.position + (direction * 9f);
        Debug.DrawRay(transform.position, runTo, Color.red);
        enemy.SetDestination(runTo);  
       
    }

    private void CheckIsRunningAway()
    {
        distanceRunTo = transform.position - runTo;
        print(distanceRunTo.magnitude);
        if (distanceRunTo.magnitude < 2.7f)
        {
            isRunning = false;
        }
    }
    public void IsHit(float damage)
    {
        if (playerInSightRange && !playerSpotted)
        {
            enemyAlerted = true;
        }

        health -= damage;
        anim.SetTrigger("isHit");
        anim.SetInteger("hitIndex", Random.Range(0, 2));

        if (health <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        if (!isDead)
        {
            enemy.isStopped = true;
            isDead = true;
            anim.SetBool("isDead", true);
            anim.SetInteger("deadIndex", Random.Range(0, 2));
            explosionEffect.Play();
            GetComponentInChildren<CoinSpawn>().SpawnCoin();
            Destroy(enemy2, 4f);
        }
    }
    private void SmoothRotation(float t, int x)
    {
        //only assign values of 1 o -1 to invert direction
        direction = (player.transform.position - transform.position).normalized * x;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, t);
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
        Gizmos.DrawLine(transform.position, walkPoint);
    }
}
