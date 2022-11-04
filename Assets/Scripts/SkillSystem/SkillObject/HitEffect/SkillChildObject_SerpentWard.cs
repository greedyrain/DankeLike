using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillChildObject_SerpentWard : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name,gameObject));
        });
    }

    public void Attack()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, SkillData.range, targetLayer);
        int index = Random.Range(0, colls.Length);
        target = colls[index].transform;

        PoolManager.Instance.GetObj("Prefabs", "SerpentWardBullet", (obj) =>
        {
            obj.GetComponent<Projectile_SerpentWard>().damage = SkillData.damage;
            obj.GetComponent<Projectile_SerpentWard>().target = target;
        });
    }
}
