using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : EnemyStates
{
 
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("You are in patrol state");         
        anim = enemy.GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        enemyNav = enemy.GetComponent<NavMeshAgent>();
        enemy2 = enemy.gameObject;
        health = maxHealth;
    }
    public override void UpdateState(EnemyStateManager enemy)
    {

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            enemyNav.SetDestination(walkPoint);

        anim.SetTrigger("patrolling");
        enemyNav.speed = walkSpeed;

        Vector3 distanceToWalkPoint = enemy2.transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
           // StartCoroutine(Idle());
            walkPointSet = false;
        }
    }

    private IEnumerator Idle()
    {
        //anim.SetTrigger("Idle");

        anim.SetTrigger("Idle");
        enemyNav.isStopped = true;
        yield return new WaitForSeconds(3);
        enemy2.transform.LookAt(walkPoint);
        enemyNav.isStopped = false;


    }

    private void SearchWalkPoint()
    {
        //
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(enemy2.transform.position.x + randomX, enemy2.transform.position.y, enemy2.transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -enemy2.transform.up, 2f, whatIsGround))
        {

            walkPointSet = true;
        }

    }
    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
    }
}
