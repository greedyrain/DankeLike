using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : BaseEnemyState
{
    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void OnFixedUpdate()
    {
        Vector2 dir = enemy.target.transform.position - enemy.transform.position;
        enemy.Move(dir);
    }

    public override void OnUpdate()
    {

    }
}
