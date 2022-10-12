using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    public override void Awake()
    {
        base.Awake();
        target = FindObjectOfType<PlayerController>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        stateMachine.Init();
        stateMachine.SwitchState(stateMachine.chaseState);
    }
}
