
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_Bedlam : BaseSkillObject 
{
    private Vector3 targetPos;
    
    public  void Update()
    {
        UniTask.WaitUntil(() => initCompleted && target != null).ContinueWith(() => { FlyToTarget(); });
    }

    public void FlyToTarget()
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
    
    public  void OnTriggerEnter(Collider collision)
    {
        if (collision.transform == target)
        {
            collision.transform.GetComponent<Enemy>().GetHurt(damage);
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
        }
    }
}
