using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    public float patrolRadius;
    private Vector2 originPos;
    [HideInInspector] public PlayerController target;
    [HideInInspector] public Vector2 patrolTarget;

    private void Update()
    {
        CheckPatrolDistance();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        originPos = transform.position;
    }

    public override void Move(Vector2 dir)
    {
        base.Move(dir);
    }

    void CheckPatrolDistance()
    {
        if (Vector2.Distance(transform.position, patrolTarget) < 1)
        {
            SwitchPatrolTarget();
        }
    }

    public void SwitchPatrolTarget()
    {
        float x = Random.Range(originPos.x - patrolRadius, originPos.x + patrolRadius);
        float y = Random.Range(originPos.y - patrolRadius, originPos.y + patrolRadius);
        patrolTarget = new Vector2(x, y);
    }
}
