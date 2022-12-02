using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitEffect_Exorcism : BaseSkillObject
{
    private Vector3 patrolTarget;
    private Transform attackTarget;
    private Vector3 attackTargetPos;
    private bool isBackToPlayer;
    private bool isAttacking;
    private bool isPatroling;

    //Check for enemy in the area that revolve around player.
    //If found enemy, set target as that enemy and move toward the target.
    //After the object makes contact with its target, the object backs to the player, and then checks for the enemy again.

    private void Update()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            FindTarget();
            SetPatrolTarget();
            
            //State Back to Player;
            if (isBackToPlayer)
                BackToPlayer();

            //State Patrol;
            if (isPatroling)
                Patrol();
            
            //State Attack;
            if (isAttacking)
                Attack();
        });
    }

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            FindTarget();
            SetPatrolTarget();
        });
    }

    public void FindTarget()
    {
        if (attackTarget == null)
        {
            Collider[] colls = Physics.OverlapSphere(owner.position, SkillData.range, targetLayer);
            if (colls.Length > 0)
                attackTarget = colls[0].transform;
            isPatroling = false;
            isAttacking = true;
            isBackToPlayer = false;
        }
    }

    void SetPatrolTarget()
    {
        if (attackTarget == null)
        {
            float x = Random.Range(owner.position.x - SkillData.radius, owner.position.x + SkillData.radius);
            float z = Random.Range(owner.position.z - SkillData.radius, owner.position.z + SkillData.radius);
            patrolTarget = new Vector3(x, 0.5f, z);
            isPatroling = true;
            isAttacking = false;
            isBackToPlayer = false;
        }
    }

    void BackToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, owner.position, SkillData.throwSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, owner.position) < 0.2f)
        {
            isPatroling = true;
            isAttacking = false;
            isBackToPlayer = false;
        }
    }

    void Patrol()
    {
        transform.position = Vector3.Slerp(transform.position, patrolTarget, SkillData.throwSpeed * Time.deltaTime);
        if (attackTarget == null && Vector3.Distance(transform.position, patrolTarget) < 0.2f)
        {
            SetPatrolTarget();
        }
    }

    void Attack()
    {
        attackTargetPos = attackTarget.position;
        transform.position = Vector3.MoveTowards(transform.position, attackTargetPos,
            SkillData.throwSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, attackTargetPos) < 0.2f)
        {
            attackTarget.GetComponent<Enemy>().GetHurt(SkillData.damage);
            isBackToPlayer = true;
            isAttacking = false;
            isPatroling = false;
        }
    }
}