using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillChildObject_SerpentWard : BaseSkillObject
{
    private Collider[] colls;
    public Transform deployPos;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.position = new Vector3(transform.position.x, 0.35f, transform.position.z);
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
            colls = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                target = colls[0].transform;
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    obj.GetComponent<HitEffect_SerpentWard>().InitData(SkillData,player);
                    obj.GetComponent<HitEffect_SerpentWard>().SetTarget(target);
                    obj.transform.position = deployPos.position;
                }); 
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}