using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Exorcism : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            Discharge();
        });
    }

    public async void Discharge()
    {
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
            {
                obj.transform.position = transform.position;
                obj.GetComponent<HitEffect_Exorcism>().InitData(SkillData,player);
                obj.GetComponent<HitEffect_Exorcism>().SetOwner(owner);
            });
            await UniTask.Delay((int) (SkillData.disposeInterval * 1000));
        }
    }
}
