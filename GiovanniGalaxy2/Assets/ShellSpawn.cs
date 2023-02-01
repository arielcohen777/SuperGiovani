using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawn : MonoBehaviour
{
    [SerializeField] private float force;
    private GameObject spawn;
    public GameObject shell;
    private GameObject shellClone;

    public bool isDead;
    public bool isSpawned;

    public Transform playerTransform;

    private Vector3 shellDirection;

    // Start is called before the first frame update
    void Start()
    {
        spawn = gameObject;
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        SpawnShell();
    }

    private void CalculateDirection()
    {
        shellDirection = (transform.position - playerTransform.position).normalized;
    }

    private void SpawnShell()
    {
        if(isDead && !isSpawned)
        {          
            shellClone = Instantiate(shell, spawn.transform.position, Quaternion.identity);
            Rigidbody rb = shellClone.GetComponent<Rigidbody>();
            rb.AddForce(shellDirection * force, ForceMode.Impulse);
            isSpawned = true;
            
        }
       
    }


}
