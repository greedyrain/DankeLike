using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitEffect_DeathWard : BaseSkillObject
{
    public int count;
    // public float speed;
    private Vector3 targetPos;

    private Collider[] selfTarget;
    
    public  void Update()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() => { MoveToTarget(); });
    }

    void MoveToTarget()
    {
        //子弹飞行逻辑
        if (target.gameObject.activeSelf)
        {
            targetPos = target.position;
            transform.forward = targetPos - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, throwSpeed * Time.deltaTime);
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
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().GetHurt(damage);
            if (count <=0)
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            }
            else
            {
                count--;
                selfTarget = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
                if (selfTarget.Length > 0)
                {
                    SetTarget(selfTarget[Random.Range(0,selfTarget.Length)].transform);
                }
            }
        }
    }
    
    public void Init(Transform target, Transform owner)
    {
        this.target = target;
        this.owner = owner;
    }
}
