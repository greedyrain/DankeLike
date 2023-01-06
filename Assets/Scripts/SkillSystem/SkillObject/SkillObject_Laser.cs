using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillObject_Laser : BaseSkillObject
{
    //设定target和owner，每一帧的位置都设定味owner的位置，朝向target的位置。
    //如果target isdead，则把target设定为空,如果owner isdead，则把owner设定为空
    private Collider[] colls;
    private async void OnEnable()
    {
        await UniTask.WaitUntil(() => initCompleted).ContinueWith(async () =>
        {
            Active(owner, SkillData.targetCount);
            await UniTask.Delay(500).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });
    }

    public async void Active(Transform owner, int count)
    {
        //初始位置与owner重合，面向target，
        Transform hitTarget = FindTarget(owner);
        if (hitTarget != null)
        {
            PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
            {
                obj.GetComponent<HitEffect_Laser>().InitData(SkillData);
                obj.GetComponent<HitEffect_Laser>().Init(hitTarget, owner);
            });
            hitTarget.GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
            if (count > 0)
            {
                await UniTask.Delay(300).ContinueWith(() => Active(hitTarget, count - 1));
            }
        }
    }
    
    //把目标和所有者放在laser身上，实时计算目标和所有者之间的位置关系
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