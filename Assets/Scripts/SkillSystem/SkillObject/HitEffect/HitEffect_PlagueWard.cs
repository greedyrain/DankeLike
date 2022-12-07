using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_PlagueWard : BaseSkillObject
{
    private Vector3 targetPos;

    public  void Update()
    {
        UniTask.WaitUntil(() => initCompleted && target != null).ContinueWith(() => { MoveToTarget(); });
    }

    void MoveToTarget()
    {
        //子弹飞行逻辑
        if (target.gameObject.activeSelf)
        {
            targetPos = target.position;
            transform.forward = targetPos - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, SkillData.throwSpeed * Time.deltaTime);
        }

        //如果子弹的目标已经死亡，则让子弹飞到目标位置后回收
        if (!target.gameObject.activeSelf || Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
            target = null;
        }
    }

    //碰到敌人后造成伤害
    public  void OnTriggerEnter(Collider collision)
    {
        if (collision.transform == target)
        {
            collision.transform.GetComponent<Enemy>().GetHurt(SkillData.damage);
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
        }
    }
}