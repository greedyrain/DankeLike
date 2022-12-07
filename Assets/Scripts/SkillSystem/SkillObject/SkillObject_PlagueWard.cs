using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_PlagueWard : BaseSkillObject 
{
    public Transform deployPos;
    private Collider[] colls;
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith( () =>
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
            colls = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                target = colls[0].transform;
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    obj.GetComponent<HitEffect_PlagueWard>().InitData(SkillData);
                    obj.GetComponent<HitEffect_PlagueWard>().SetTarget(target);
                    obj.transform.position = deployPos.position;
                }); 
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}
