using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    public override void Awake()
    {
        base.Awake();
        target = FindObjectOfType<PlayerController>();
        Debug.Log(target.name);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        stateMachine.Init();
        stateMachine.SwitchState(stateMachine.chaseState);
    }
}
