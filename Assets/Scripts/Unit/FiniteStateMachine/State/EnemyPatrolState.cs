using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatrolState : BaseEnemyState
{
    public override void OnEnter()
    {
        //enemy.anim.Play("Idle");
        SwitchPatrolTarget();
    }

    public override void OnExit()
    {
        
    }

    public override void OnFixedUpdate()
    {
        Vector2 dir = enemy.patrolTarget - (Vector2)enemy.transform.position;
        enemy.Move(dir);
    }

    public override void OnUpdate()
    {
        CheckPatrolDistance();
    }

    void CheckPatrolDistance()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.patrolTarget) < 1)
        {
            SwitchPatrolTarget();
        }
    }

    public void SwitchPatrolTarget()
    {
        float x = Random.Range(enemy.originPos.x - enemy.patrolRadius, enemy.originPos.x + enemy.patrolRadius);
        float y = Random.Range(enemy.originPos.y - enemy.patrolRadius, enemy.originPos.y + enemy.patrolRadius);
        enemy.patrolTarget = new Vector2(x, y);
    }
}
