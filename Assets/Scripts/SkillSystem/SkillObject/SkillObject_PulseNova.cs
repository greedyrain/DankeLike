using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_PulseNova : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            Attack();
            float duration = owner.GetComponent<PlayerController>().CalculateDuration(SkillData.duration);
            UniTask.Delay((int) (duration * 1000))
                .ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });
    }

    void Attack()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, SkillData.range, targetLayer);
        if (colls.Length > 0)
        {
            foreach (var enemy in colls)
            {
                enemy.GetComponent<Enemy>().GetHurt(owner.GetComponent<PlayerController>().CalculateDamage(SkillData.damage));
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    obj.transform.position = enemy.transform.position;
                    float duration = owner.GetComponent<PlayerController>().CalculateDuration(SkillData.duration);
                    UniTask.Delay((int) (duration * 1000))
                        .ContinueWith(() => PoolManager.Instance.PushObj(obj.name, obj));
                });
            }
        }
    }
}