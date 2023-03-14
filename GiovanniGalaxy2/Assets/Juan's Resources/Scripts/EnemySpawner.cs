using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject [] enemy;     
    public Transform [] spawnPoint;

    public int enemyCtr = 0;
    public int maxEnemy;
    private bool isSpawning;
 
 
    // Update is called once per frame
    void Update()
    {
        if(!isSpawning && enemyCtr < maxEnemy)
        {
            StartCoroutine(SpawnEnemies());
        }        
    }
    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, spawnPoint[Random.Range(0, spawnPoint.Length)].rotation);
        enemyCtr++;
        yield return new WaitForSeconds(2);
        isSpawning =false;
    }
    public void EnemyDestroyed()
    {
        enemyCtr--;
    }
}
