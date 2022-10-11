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
        enemy.patrolTarget.position = enemy.originPos;
    }

    public override void OnFixedUpdate()
    {
        Vector2 dir = enemy.patrolTarget.position - enemy.transform.position;
        enemy.Move(dir);
    }

    public override void OnUpdate()
    {
        CheckPatrolDistance();
    }

    void CheckPatrolDistance()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.patrolTarget.position) < 1)
        {
            SwitchPatrolTarget();
        }
    }

    public void SwitchPatrolTarget()
    {
        float x = Random.Range(enemy.originPos.x - enemy.enemyData.patrolRadius, enemy.originPos.x + enemy.enemyData.patrolRadius);
        float y = Random.Range(enemy.originPos.y - enemy.enemyData.patrolRadius, enemy.originPos.y + enemy.enemyData.patrolRadius);

        enemy.patrolTarget.position = new Vector2(x, y);
    }
}
