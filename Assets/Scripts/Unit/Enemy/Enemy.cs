using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    public float patrolRadius;

    public float alertRadius;
    public LayerMask targetLayer;

    public float attackRadius;
    public float attackCD;
    [HideInInspector] public float remainAttackCD;

    [HideInInspector] public Vector2 originPos;
    public Transform patrolTarget;

    [HideInInspector] public EnemyStateMachine stateMachine;
    [HideInInspector] public PlayerController target;

    public override void Awake()
    {
        base.Awake();
        Debug.Log(transform.parent.name);
        stateMachine = GetComponent<EnemyStateMachine>();
    }
}
