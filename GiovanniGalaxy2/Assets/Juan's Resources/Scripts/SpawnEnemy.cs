using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy2Prefab;
    public int maxEnemies;
    public int enemiesAlive, totalEnemiesAlive;
    private bool enable;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemyCounter", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy2(enable);
    }

    private void EnemyCounter()
    {
        enemiesAlive = GameObject.FindGameObjectsWithTag("Dummie").Length;
        totalEnemiesAlive = enemiesAlive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            enable = true;
        }         
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
          
            enable = false;
        }
    }

    public bool SpawnEnemy2(bool enable)
    {
        if (!enable)
        {
            return false;
        }

        else
        {
            while (enemiesAlive != maxEnemies)
            {
                Instantiate(enemy2Prefab, transform.position, transform.rotation);
                enemiesAlive++;
            }

            return true;
        }

    }
}
