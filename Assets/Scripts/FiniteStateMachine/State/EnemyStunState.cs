using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : BaseEnemyState
{
    float tempTotalMovespeed;

    public override void OnEnter()
    {
        //enemy.anim.Play("Stun");
        tempTotalMovespeed = enemy.totalMoveSpeed;
        enemy.totalMoveSpeed = 0;
    }

    public override void OnExit()
    {
        enemy.totalMoveSpeed = tempTotalMovespeed;
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
    }
}
