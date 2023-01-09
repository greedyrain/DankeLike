using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Scorched : BaseSkillObject
{
    private  void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.localScale = new Vector3(SkillData.radius * 2, 0.05f, SkillData.radius * 2); 
            Guarantee();
            UniTask.Delay((int) (duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });
    }

    public async void Guarantee()
    {
        Debug.Log("Radius is :"+SkillData.radius);
        float time = duration; 
        while (time > 0)
        {
            await UniTask.Delay((int) (SkillData.actionInterval * 1000)).ContinueWith(()=>
            {
                time -= SkillData.actionInterval;
                Collider[] colls = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
                if (colls.Length > 0)
                {
                    for (int i = 0; i < colls.Length; i++)
                        colls[i].GetComponent<Enemy>()?.GetHurt(damage);
                }
            });
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,SkillData.radius);
    }
}
