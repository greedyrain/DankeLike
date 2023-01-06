using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_SplitEarth : BaseSkillObject
{
    private int count;
    private List<Collider> targetColls = new List<Collider>();
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.rotation =Quaternion.identity; 
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Active(transform.position,SkillData.count,SkillData.radius);
            UniTask.Delay((int) (SkillData.duration * 100)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });
    }

    public async void Active(Vector3 pos, int count, float radius)
    {
        PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
        {
            obj.transform.position = pos;
            obj.transform.localScale = new Vector3(radius*2, transform.localScale.y, radius*2);
            obj.GetComponent<HitEffect_SplitEarth>().InitData(SkillData);
        });
        Collider[] colls = Physics.OverlapSphere(pos,radius, targetLayer);
        for (int i = 0; i < colls.Length; i++)
        {
            targetColls.Add(colls[i]);
        }
        colls = null;
        
        for (int i = 0; i < targetColls.Count; i++)
        {
            targetColls[i].GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
        }
        targetColls.Clear();

        
        if (count > 0)
        {
            await UniTask.Delay((int)(SkillData.disposeInterval*1000)).ContinueWith(() => Active(transform.position, count - 1,radius*1.5f));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,SkillData.radius);
    }
}