using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    public override void Awake()
    {
        base.Awake();
        enemyData = GameDataManager.Instance.EnemyData[0];
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stateMachine.Init();
        originPos = transform.position;
        stateMachine.SwitchState(stateMachine.patrolState);
    }

    private void Update()
    {
        Alert();
    }

    public void Alert()
    {
        if (Physics2D.OverlapCircle(transform.position, enemyData.alertRadius, targetLayer))
        {
            target = Physics2D.OverlapCircle(transform.position, enemyData.alertRadius, targetLayer).GetComponent<PlayerController>();
            stateMachine.SwitchState(stateMachine.chaseState);
        }
        else if (target != null)
        {
            target = null;
            stateMachine.SwitchState(stateMachine.patrolState);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(originPos, patrolRadius);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, alertRadius);
    //}
}
