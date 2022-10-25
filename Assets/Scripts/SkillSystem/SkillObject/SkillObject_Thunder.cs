using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Thunder : BaseSkillObject 
{
    
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.localScale = Vector3.one * SkillData.radius * 2;
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
            Thunder();
        });
    }

    public void Thunder()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, SkillData.radius, targetLayer);
        if (colls.Length > 0)
        {
            for (int i = 0; i < colls.Length; i++)
                colls[i].GetComponent<Enemy>()?.GetHurt(SkillData.damage);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,SkillData.radius);
    }
}
