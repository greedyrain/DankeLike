using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_EyeOfTheStorm : BaseSkillObject
{
    public List<Transform> targetList = new List<Transform>();
    private Collider2D[] colls;
    private int targetCount;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            // SetTargetNum();
            SelectTarget();
            UniTask.Delay((int)(SkillData.duration*1000)).ContinueWith(()=>{PoolManager.Instance.PushObj(gameObject.name, gameObject);});
        });
    }

    public async void Lightning(Transform target)
    {
        PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
        {
            obj.GetComponent<HitEffect_Lightning>().InitData(SkillData);
            obj.GetComponent<HitEffect_Lightning>().Init(target, owner);
        });
        target.GetComponent<Enemy>().GetHurt(SkillData.damage);
    }

    public async void SelectTarget()
    {
        int index;
        while (gameObject.activeSelf)
        {
            colls = Physics2D.OverlapCircleAll(transform.position, SkillData.range, targetLayer);
            if (colls.Length > 0)
            {
                for (int i = 0; i < SkillData.count; i++)
                {
                    index = Random.Range(0, colls.Length);
                    targetList.Add(colls[index].transform);
                }
            }

            for (int i = 0; i < targetList.Count; i++)
            {
                Lightning(targetList[i]);
            }

            colls = null;
            targetList.Clear();
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }

    // public void SetTargetNum()
    // {
    //     switch (SkillData.level)
    //     {
    //         case 1:
    //             targetCount = 1;
    //             break;
    //         case 2:
    //             targetCount = 2;
    //             break;
    //         case 3:
    //             targetCount = 3;
    //             break;
    //         case 4:
    //             targetCount = 4;
    //             break;
    //         case 5:
    //             targetCount = 5;
    //             break;
    //     }
    // }
}