using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    Enemy enemy;

    public void OnEnter()
    {
        enemy.anim.Play("Idle");
    }

    public void OnExit()
    {
        
    }

    public void OnFixedUpdate()
    {

    }

    public void OnUpdate()
    {
        Vector2 dir = enemy.patrolTarget - (Vector2)enemy.transform.position;
        enemy.Move(dir);
    }
}
