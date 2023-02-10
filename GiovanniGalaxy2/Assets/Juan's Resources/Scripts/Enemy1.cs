using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{

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

    private GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
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
        if(isOnWall)
        {
            anim.SetBool("isClimbing", true);
            navMesh.baseOffset = 0.3f;
            transform.Rotate(90,0,0);
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

            Transform hitter = (rndIdx == 1) ? attackPoints[0] : attackPoints[1];

            Collider[] hitPlayer = Physics.OverlapSphere(hitter.position, attackRange, whatIsPlayer);

            foreach (Collider player in hitPlayer)
            {
                //Debug.Log(player.tag);
                player.GetComponentInParent<Health>().IsHit(damage);
            }
        }        
    }

    public void IsHit(float damage)
    {     
        health -= damage;
        anim.SetTrigger("isHit");

        Debug.Log(health); 

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        navMesh.isStopped = true;
        isDead = true;
        anim.SetTrigger("isDead");       
        Destroy(enemy, 0.1f);
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoints == null)
            return; 

        foreach(Transform t in attackPoints)
            Gizmos.DrawWireSphere(t.position, attackRange); 
    }

}
