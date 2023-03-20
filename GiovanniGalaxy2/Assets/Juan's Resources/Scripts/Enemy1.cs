using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    //SAVS - Particle System ref 
    private ParticleSystem explosionEffect;

    [Header("Navigation")]
    private NavMeshAgent navMesh;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer, whatIsObstruction;
    // anim
    private Animator anim;
    [SerializeField] private Vector3 rotation;

    //checks
    public bool isOnWall;
    public bool playerReached;

    // health 
    [Header("Set Enemy Health Stats")]
    public const float maxHealth = 100;
    public float health;
    public int damage = 5;

    private GameObject enemy;
    public bool isDead;

    //attack 
    public Transform[] attackPoints;
    public float attackRange = 0.5f;
    public LayerMask playerLyr;

    private GameManager gm;  


    // Start is called before the first frame update
    void Start()
    {
        explosionEffect = GetComponentInChildren<ParticleSystem>();
        gm = GameManager.Instance;
        //player = gm.player.transform;
        player = gm.player.transform; 
        anim = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        health = maxHealth;
        enemy = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        Climbing();
        Attack();
    }

    public void ReachedPlayer()
    {
        float distance = Vector3.Distance(gameObject.transform.position, player.position);
        playerReached = distance <= 2.1f;
    }

    private void Chase()
    {
        ReachedPlayer();

        if (!playerReached)
        {
            anim.SetBool("isFollowing", true);
            navMesh.SetDestination(player.position);
        }

        else
        {
            anim.SetBool("isFollowing", false);
        }
    }

    private void Climbing()
    {
        if (navMesh.isOnOffMeshLink)
        {
            navMesh.speed = 4f;
            Vector3 direction = (player.transform.position - transform.position).normalized;  
            navMesh.velocity = direction * navMesh.speed;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);
            navMesh.isStopped = true ;
        }
        if (isOnWall && !navMesh.isOnOffMeshLink)
        {
            navMesh.speed = 6f;
            anim.SetBool("isClimbing", true);
            navMesh.baseOffset = 0.3f;
            transform.Rotate(90, 0, 0);
            navMesh.isStopped = false;
        }
        else
        {
            navMesh.speed = 4f;
            anim.SetBool("isClimbing", false);
            navMesh.baseOffset = -0.06f;
            transform.Rotate(0, 0, 0);
            navMesh.isStopped = false;
        }
    }

    // This anim is called from the attack Animation when it hits the player 
    public void PunchAnimationEvent()
    {

        //Debug.Log("player is hit");
        //player.gameObject.GetComponent<Health>().IsHit(damage);



        // codigo para hacerle dano al jugador 
        // checar si esta en el area cuando lanza el golpe 
    }
    private void Attack()
    {
        if (playerReached)
        {
            //Debug.Log("is attacking works");
            //anim.SetBool("isAttacking", true);
            //transform.LookAt(player);

            // plays a random animation for attacking
            int rndIdx = Random.Range(1, 4);
            anim.SetInteger("attackAnimID", rndIdx);
            anim.SetTrigger("attack");
            navMesh.SetDestination(transform.position);

            //Transform hitter = (rndIdx == 1) ? attackPoints[0] : attackPoints[1];

            //Collider[] hitPlayer = Physics.OverlapSphere(hitter.position, attackRange, playerLyr);
        }
    }

    public void IsHit(float damage)
    {
        health -= damage;
        anim.SetTrigger("isHit");

        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    private void Death()
    {
        if (!isDead)
        {
            navMesh.isStopped = true;
            isDead = true;
            anim.SetTrigger("isDead");
            explosionEffect.Play();
            GetComponentInChildren<CoinSpawn>().SpawnCoin();
            gm.enemySpawner.EnemyDestroyed();
            Destroy(enemy, 0.1f);
            gm.enemySpawner.EnemyDestroyed();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
            isOnWall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
            isOnWall = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoints == null)
            return;

        foreach (Transform t in attackPoints)
            Gizmos.DrawWireSphere(t.position, attackRange);
    }

}