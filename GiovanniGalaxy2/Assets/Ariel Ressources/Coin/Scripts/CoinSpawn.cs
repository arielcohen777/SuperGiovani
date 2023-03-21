using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    public Transform prefabToSpawn;
    public float spawnRadius = 3;

    //This will spawn a coin at the CoinSpawn object
    //Call this function wherever needed in other scripts
    public void SpawnCoin()
    {
        Transform coin = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        Rigidbody cRB = coin.GetComponent<Rigidbody>();
        cRB.AddForce(Random.insideUnitSphere * spawnRadius, ForceMode.Impulse);
    }
}
