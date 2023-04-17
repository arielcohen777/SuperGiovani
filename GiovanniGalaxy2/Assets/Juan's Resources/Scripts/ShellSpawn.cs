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

    private Vector3 shellDirection;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        spawn = gameObject;
        enemy = GetComponentInParent<Enemy1>();
        damage = 100f;
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        SpawnShell();
    }

    private void CalculateDirection()
    {
        shellDirection = (gameObject.transform.forward)*-1;
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
           enemy.hitByJump = true;
           enemy.IsHit(damage);
           hitJump = true;
        }      
    }
   
}
