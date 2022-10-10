using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    public float patrolRadius;
    [HideInInspector] public Vector2 originPos;
    [HideInInspector] public Vector2 patrolTarget;

    [HideInInspector] public EnemyStateMachine stateMachine;
    [HideInInspector] public PlayerController target;

    public override void Awake()
    {
        base.Awake();
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        originPos = transform.position;
    }
}
