using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillObject_ParalyzingCask : BaseSkillObject
{
    public int count;
    private Vector2 targetPos;
    private Collider2D[] selfTarget;

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
            transform.right = targetPos - (Vector2) transform.position;
            transform.position =
                Vector2.MoveTowards(transform.position, targetPos, SkillData.throwSpeed * Time.deltaTime);
        }

        //如果子弹的目标已经死亡，则让子弹飞到目标位置后回收
        if (!target.gameObject.activeSelf || Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
            target = null;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().GetHurt(SkillData.damage);
            if (count <= 0)
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            }
            else
            {
                count--;
                selfTarget = Physics2D.OverlapCircleAll(transform.position, SkillData.radius, targetLayer);
                if (selfTarget.Length > 0)
                {
                    SetTarget(selfTarget[Random.Range(0, selfTarget.Length)].transform);
                }
            }
        }
    }
}