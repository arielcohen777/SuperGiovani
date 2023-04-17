using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyStates currentState; // holds a reference to the active staten in the state machine

    public EnemyPatrol patrolState = new EnemyPatrol();
    public EnemyChase chaseState = new EnemyChase();
    public EnemyAttack attackState = new EnemyAttack();
    public EnemyFlee fleeState = new EnemyFlee();

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyStates state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
