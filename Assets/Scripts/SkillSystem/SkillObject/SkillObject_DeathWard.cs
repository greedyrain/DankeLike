using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_DeathWard : BaseSkillObject
{
    public Transform deployPos;
    private Collider[] colls;
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(async () =>
        {
            transform.position = new Vector3(transform.position.x, 1.334f, transform.position.z);
            UniTask.Delay((int) (duration * 1000))
                .ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
            while (gameObject.activeSelf)
            {
                Attack(deployPos,SkillData.targetCount);
                await UniTask.Delay((int) (SkillData.actionInterval * 1000));
            }
        });
    }

    //蛇棒
    public void Attack(Transform owner, int count)
    {
        //初始位置与owner重合，面向target，
        Transform hitTarget = FindTarget(owner);
        if (hitTarget != null)
        {
            PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
            {
                obj.transform.position = owner.position;
                obj.GetComponent<HitEffect_DeathWard>().count = count;
                obj.GetComponent<HitEffect_DeathWard>().InitData(SkillData,player);
                obj.GetComponent<HitEffect_DeathWard>().Init(hitTarget, owner);
            });
        }
    }
    
    public Transform FindTarget(Transform owner)
    {
        colls = Physics.OverlapSphere(owner.position, SkillData.range, targetLayer);
        if (colls.Length > 0)
        {
            int index = Random.Range(0, colls.Length);
            Transform hitTarget = colls[index].transform;
            if (hitTarget == owner && colls.Length > 1)
            {
                FindTarget(owner);
            }
            else if (hitTarget == owner && colls.Length <= 1)
            {
                return null;
            }
            return hitTarget;
        }
        return null;
    }
}