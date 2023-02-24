using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyFlee : EnemyStates
{
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("You are in patrolling state");
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
    }
    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
    }
}
