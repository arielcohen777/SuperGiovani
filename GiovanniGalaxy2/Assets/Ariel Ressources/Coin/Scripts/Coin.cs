using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gm;
    
    private void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("Weapon"))
        {
            gm.playerStuff.UpdateCoin();
            Destroy(gameObject);
        }
    }
}
