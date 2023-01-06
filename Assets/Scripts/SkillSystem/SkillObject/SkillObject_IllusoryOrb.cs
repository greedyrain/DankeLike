using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_IllusoryOrb : BaseSkillObject
{
    public TrailRenderer trail;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            trail.enabled = true;

            UniTask.Delay((int) (SkillData.duration * 1000) - 1000).ContinueWith(() =>
            {
                trail.enabled = false;
            });
            UniTask.Delay((int) (SkillData.duration * 1000))
                .ContinueWith(() => { PoolManager.Instance.PushObj(gameObject.name, gameObject); });
        });
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * SkillData.throwSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy") && initCompleted)
        {
            col.GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
        }
    }
}