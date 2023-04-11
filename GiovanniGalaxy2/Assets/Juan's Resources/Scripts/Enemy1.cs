using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool hitByJump = false;

    private GameObject enemy;
    public bool isDead;

    //attack 
    public Transform[] attackPoints;
    public float attackRange = 0.5f;
    public LayerMask playerLyr;
    private GameManager gm;

    //audiosource
    private AudioSource audioSource;
    public AudioClip explosion;
    public AudioClip killJump;
    public AudioClip attack;
    public AudioClip [] chase;
    public AudioClip[] hit; 
    

    // Start is called before the first frame update
    void Start()
    {
       
        audioSource = GetComponentInChildren<AudioSource>();
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

            if (!audioSource.isPlaying)
            {
                audioSource.clip = chase[Random.Range(0, chase.Length)];
                audioSource.volume = 0.3f;
                audioSource.maxDistance = 15f;
                audioSource.PlayDelayed(Random.Range(3f, 10f));
            }
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
            // 
            Vector3 direction = (player.transform.position - transform.position).normalized;
            navMesh.velocity = direction * navMesh.speed;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);

        }
        if (isOnWall && !navMesh.isOnOffMeshLink)
        {
            anim.SetBool("isClimbing", true);
            navMesh.baseOffset = 0.3f;
            transform.Rotate(90, 0, 0);
        }
        else
        {
            anim.SetBool("isClimbing", false);
            navMesh.baseOffset = -0.06f;
        }
    }

    // This anim is called from the attack Animation when it hits the player 
    public void PunchAnimationEvent()
    {

        //Debug.Log("player is hit");
        //player.gameObject.GetComponent<Health>().IsHit(damage);
            audioSource.clip = attack;
            audioSource.volume = 0.5f;
            audioSource.Play();
        
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
        if (health > 0)
        {
            anim.SetTrigger("isHit");
            audioSource.clip = hit[Random.Range(0, hit.Length)];
            audioSource.volume = 0.4f;             
            audioSource.Play();
        }     

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
            audioSource.volume = 1f;

            if (hitByJump)
            {
                audioSource.clip = killJump;
                audioSource.Play();
            }

            else
            {
                audioSource.clip = explosion;
                audioSource.maxDistance = 50;
                audioSource.Play();
            }

            explosionEffect.Play();

            anim.SetTrigger("isDead");           
            GetComponentInChildren<CoinSpawn>().SpawnCoin();
            gm.enemySpawner.EnemyDestroyed();
 
            Destroy(enemy, 0.1f);

            isDead = true;
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