using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawn : MonoBehaviour
{
    [SerializeField] private float force;
    private float damage;
    private bool hitJump;
    private GameObject spawn;
    public GameObject shell;
    private GameObject shellClone;

    private Enemy1 enemy;

   // public bool isDead;
    private bool isSpawned;

    public Transform playerTransform;

    private Vector2 shellDirection;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        spawn = gameObject;
        playerTransform = GameObject.Find("PlayerMovement").transform;
        enemy = GetComponentInParent<Enemy1>();
        damage = 100f;
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        SpawnShell();
    }

    private void CalculateDirection()
    {
        Vector2 objectTransform = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetTransform = new Vector2(playerTransform.position.x, playerTransform.position.z);
        shellDirection = (objectTransform - targetTransform).normalized;
    }

    private void SpawnShell()
    {
        if(hitJump && enemy.isDead && !isSpawned)
        {          
            shellClone = Instantiate(shell, spawn.transform.position, Quaternion.identity);
            Rigidbody rb = shellClone.GetComponent<Rigidbody>();
            rb.AddForce(shellDirection * force, ForceMode.Impulse);
            isSpawned = true;
            
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (!hitJump && other.CompareTag("Player"))
        {
           enemy.IsHit(damage);
           hitJump = true;
        }      
    }
   
}
