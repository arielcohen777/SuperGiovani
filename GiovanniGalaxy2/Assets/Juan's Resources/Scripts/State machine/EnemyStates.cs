 using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyStates 
{
    //SAVS - Particle System ref 
    public ParticleSystem explosionEffect;
    //
    [Header("Fov")]
    public float viewAngle;

    //

    public NavMeshAgent enemyNav;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsObstruction;

    // patrol
    [Header("Patrol")]
    public float walkPointRange;
    public Vector3 walkPoint;
    public bool walkPointSet;


    //attack
    [Header("Enemy Projectile")]
    public GameObject projectile;
    public GameObject spawnPoint;
    public GameObject cloneProjectile;

    //flee
    public Vector3 direction, distanceRunTo, runTo;
    public bool isRunning;

    //health system     

    [Header("Set Enemy Health Stats")]
    public const int maxHealth = 100;
    public int health;

    public GameObject enemy2;
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

    [SerializeField]
    private bool
        playerInSightRange, playerSpotted, playerInAttackRange, playerInFleeRange, enemyAlerted;

    // anim
    public Animator anim;
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);
    public abstract void OnCollisionEnter(EnemyStateManager enemy);
}
