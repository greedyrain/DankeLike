using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_PlasmaField : BaseSkillObject
{
    private ParticleSystem particle;
    private SphereCollider coll;
    private void OnEnable()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        coll = GetComponent<SphereCollider>();
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name,gameObject);
            });
        });
    }

    private void Update()
    {
        coll.radius = particle.shape.radius;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
        }
    }

    public void Recycle()
    {
        PoolManager.Instance.PushObj(gameObject.name,gameObject);
    }
}
