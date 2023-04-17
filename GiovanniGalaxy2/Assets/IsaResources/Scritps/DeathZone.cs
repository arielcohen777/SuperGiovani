using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Coin"))
            Destroy(player.gameObject);
        player.transform.position = respawnPoint.transform.position;
    }
}
