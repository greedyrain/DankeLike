using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        stateMachine.Init();
        stateMachine.SwitchState(stateMachine.patrolState);
    }

    private void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(originPos, patrolRadius);
    }
}
