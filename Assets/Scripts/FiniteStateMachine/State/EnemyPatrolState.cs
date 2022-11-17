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
        enemy.patrolPos = enemy.originPos;
    }

    public override void OnFixedUpdate()
    {
        Vector3 dir = (enemy.patrolPos - enemy.transform.position).normalized;
        enemy.Move(dir);
    }

    public override void OnUpdate()
    {
        CheckPatrolDistance();
    }

    void CheckPatrolDistance()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.patrolPos) < 0.1f)
        {
            SwitchPatrolTarget();
        }
    }

    public void SwitchPatrolTarget()
    {
        float x = Random.Range(enemy.originPos.x - enemy.enemyData.patrolRadius, enemy.originPos.x + enemy.enemyData.patrolRadius);
        float z = Random.Range(enemy.originPos.z - enemy.enemyData.patrolRadius, enemy.originPos.z + enemy.enemyData.patrolRadius);

        enemy.patrolPos = new Vector3(x, 0,z);
    }
}
