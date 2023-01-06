using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillObject_ParalyzingCask : BaseSkillObject
{
    public int count;
    private Vector3 targetPos;
    private Collider[] selfTarget;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() => count = SkillData.targetCount);
    }

    public void Update()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            if (target != null)
                MoveToTarget();
            else
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
        });
    }

    void MoveToTarget()
    {
        //子弹飞行逻辑
        if (target.gameObject.activeSelf)
        {
            targetPos = target.position;
            transform.forward = targetPos - transform.position;
            transform.position =
                Vector3.MoveTowards(transform.position, targetPos, SkillData.throwSpeed * Time.deltaTime);
        }

        //如果子弹的目标已经死亡，则让子弹飞到目标位置后回收
        if (!target.gameObject.activeSelf || Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
            target = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(count);
            other.transform.GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
            if (count <= 0)
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            }
            else
            {
                count--;
                selfTarget = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
                if (selfTarget.Length > 0)
                {
                    SetTarget(selfTarget[Random.Range(0, selfTarget.Length)].transform);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,SkillData.radius);
    }
}