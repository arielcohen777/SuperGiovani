using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    GameManager gm;
    public bool hasHit = true;
    private Enemy1 enemy;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        enemy = GetComponentInParent<Enemy1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasHit)
            {
                hasHit = false;
                Debug.Log("has hit");
                //gm.playerHealth.GetComponent<Health>().IsHit(enemy.damage);
                //other.gameObject.GetComponent<Health>().IsHit(enemy.damage);
                gm.player.GetComponent<Health>().IsHit(enemy.damage);
                //Debug.Log("Player hit");
                StartCoroutine(CanHitAgain());
            }
        }
    }

    IEnumerator CanHitAgain()
    {
        yield return new WaitForSeconds(0.5f);
        hasHit = true;         
    }

    // Update is called once per frame
    void Update()
    {

    }
}