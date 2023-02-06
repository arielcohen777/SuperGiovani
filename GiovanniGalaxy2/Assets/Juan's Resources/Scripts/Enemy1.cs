using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{

    [Header("Navigation")]
    private NavMeshAgent enemy;
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

    private GameObject enemy1;
    public bool isDead;

   /* public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask layer; */

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<NavMeshAgent>();
        enemy1 = gameObject;
        health = maxHealth;
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
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (distance <= 2.1f)
            playerReached = true;
        else
            playerReached = false;
    }

    private void Chase()
    {
        ReachedPlayer();

        if (!playerReached)
        {
            anim.SetBool("isFollowing", true);
            enemy.SetDestination(player.position);
        }

       else
        {
            anim.SetBool("isFollowing", false);
        }
    }

    private void Climbing()
    {
        if(isOnWall)
        {
            anim.SetBool("isClimbing", true);
            enemy.baseOffset = 0.3f;
            transform.Rotate(90,0,0);
        }
        else
        {
            anim.SetBool("isClimbing", false);
            enemy.baseOffset = -0.06f;
        }
    }

    // This anim is called from the attack Animation when it hits the player 
    public void PunchAnimationEvent()
    {
       
            Debug.Log("player is hit");
            player.gameObject.GetComponent<Health>().IsHit(damage);
       
       

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
            anim.SetInteger("attackAnimID", Random.Range(0, 3));
            anim.SetTrigger("attack");
            enemy.SetDestination(transform.position);
        }        
    }

    public void IsHit(float damage)
    {     
        health -= damage;
        anim.SetTrigger("isHit");
    
        if (health <= 0)
            Death();
    }

    private void Death()
    {
        enemy.isStopped = true;
        isDead = true;
        anim.SetTrigger("isDead");       
        Destroy(enemy1, 4f);
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Wall"))
            isOnWall = true;  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
            isOnWall = false;
    }

  
}
