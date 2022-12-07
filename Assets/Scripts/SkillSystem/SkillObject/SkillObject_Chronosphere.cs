using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Chronosphere : BaseSkillObject
{
    private Collider[] colls;
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.localScale = Vector3.one * SkillData.radius * 2;
            Attack();
            UniTask.Delay((int) (SkillData.duration * 1000))
                .ContinueWith(() =>
                {
                    Collider[] enemys = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
                    if (colls.Length > 0)
                    {
                        foreach (var target in enemys)
                        {
                            target.GetComponent<Enemy>().ResetMoveSpeed();
                        }
                    }
                    PoolManager.Instance.PushObj(gameObject.name, gameObject);
                });
        });
    }

    async void Attack()
    {
        while (gameObject.activeSelf)
        {
            colls = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                foreach (var target in colls)
                {
                    target.GetComponent<Enemy>().GetHurt(SkillData.damage);
                }
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().SetMoveSpeed(0);
        }
    }
}
