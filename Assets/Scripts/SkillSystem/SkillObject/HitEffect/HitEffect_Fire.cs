using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_Fire : BaseSkillObject
{
    BuffData buffData = new BuffData();
    
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            InitBuffData();
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.AddBuff(buffData);
        }
    }

    public void InitBuffData()
    {
        buffData.damage = SkillData.damage;
        buffData.interval = SkillData.interval;
        buffData.duration = SkillData.duration;
        buffData.id = SkillData.ID;
        buffData.buffName = SkillData.skillName;
        buffData.remainTime = SkillData.duration;
        buffData.onDispose += () =>
        {
            
        };
    }
}