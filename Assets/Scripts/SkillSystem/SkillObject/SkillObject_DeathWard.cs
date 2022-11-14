using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_DeathWard : BaseSkillObject
{
    // private Collider2D[] colls;
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(async () =>
        {
            Debug.Log(SkillData == null);
            Attack();
            UniTask.Delay((int) (SkillData.duration * 1000))
                .ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });

    }

    //蛇棒
    public async void Attack()
    {
        while (gameObject.activeSelf)
        {
            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                target = colls[0].transform;
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    obj.GetComponent<HitEffect_SerpentWard>().InitData(SkillData);
                    obj.GetComponent<HitEffect_SerpentWard>().SetTarget(target);
                    obj.transform.position = transform.position;
                });
                // if (count > 0)
                //     Attack(target,count - 1);
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}