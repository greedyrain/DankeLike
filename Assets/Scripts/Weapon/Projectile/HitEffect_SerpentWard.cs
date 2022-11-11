using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_SerpentWard : BaseSkillObject
{
    public float speed;
    private Vector2 targetPos;

    public  void Update()
    {
        UniTask.WaitUntil(() => initCompleted != null).ContinueWith(() => { MoveToTarget(); });
    }

    void MoveToTarget()
    {
        if (target.gameObject.activeSelf)
        {
            targetPos = target.position;
            transform.right = targetPos - (Vector2)transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        if (!target.gameObject.activeSelf || Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
            target = null;
        }
    }

    public  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().GetHurt(SkillData.damage);
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
        }
    }
}