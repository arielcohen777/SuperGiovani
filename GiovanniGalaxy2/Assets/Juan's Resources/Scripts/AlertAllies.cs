using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class AlertAllies : MonoBehaviour
{
    private Enemy2 enemyScript;
    private GameObject enemy;
    public LayerMask whatIsAlly;
    public float alertRange;
    [SerializeField] private bool alliesInRange;


    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy2>();
        //enemy = gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        alliesInRange = Physics.CheckSphere(transform.position, alertRange, whatIsAlly);
    
        AlertArea();

         
    }

    public void AlertArea()
    {
        if (enemyScript.playerSpotted && alliesInRange)
        {
            Collider[] numberAlliesInRange = Physics.OverlapSphere(transform.position, alertRange, whatIsAlly);
            foreach (Collider enemy in numberAlliesInRange)
            {
                enemy.gameObject.GetComponent<Enemy2>().enemyAlerted = true;
            }
        }       
      
    }

    private void OnDrawGizmosSelected()
    {
       if (!alliesInRange)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, alertRange);
        }
        else
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, alertRange);
        }        

    }
}
