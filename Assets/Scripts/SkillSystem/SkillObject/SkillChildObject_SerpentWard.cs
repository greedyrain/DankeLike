using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillChildObject_SerpentWard : BaseSkillObject
{
    private Collider2D[] colls;

    private void OnEnable()
    {

        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            Attack();
            UniTask.Delay((int) (SkillData.duration * 1000))
                .ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });
    }
    
    public async void Attack()
    {
        while (gameObject.activeSelf)
        {
            colls = Physics2D.OverlapCircleAll(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                target = colls[0].transform;
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    obj.GetComponent<HitEffect_SerpentWard>().InitData(SkillData);
                    obj.GetComponent<HitEffect_SerpentWard>().SetTarget(target);
                    obj.transform.position = transform.position;
                }); 
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}