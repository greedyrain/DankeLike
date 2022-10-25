using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Shield : BaseSkillObject
{
    private  void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.localScale = Vector3.one * SkillData.radius * 2;
            Guarantee();
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });
    }

    public async void Guarantee()
    {
        Debug.Log("Radius is :"+SkillData.radius);
        float time = SkillData.duration;
        while (time > 0)
        {
            await UniTask.Delay((int) (SkillData.actionInterval * 1000)).ContinueWith(()=>
            {
                time -= SkillData.actionInterval;
                Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, SkillData.radius, targetLayer);
                if (colls.Length > 0)
                {
                    for (int i = 0; i < colls.Length; i++)
                        colls[i].GetComponent<Enemy>()?.GetHurt(SkillData.damage);
                }
            });
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,SkillData.radius);
    }
}
