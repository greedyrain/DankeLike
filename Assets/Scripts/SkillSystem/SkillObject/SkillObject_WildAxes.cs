using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_WildAxes : BaseSkillObject 
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            owner = FindObjectOfType<PlayerController>().transform;
            DisposeAxes();
            UniTask.Delay(1000).ContinueWith(()=>PoolManager.Instance.PushObj(gameObject.name, gameObject));
        }); 
    }

    public void DisposeAxes()
    {
        if (target != null)
        {

            for (int i = 0; i < 2; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    Debug.Log("--------------------------------------");
                    obj.GetComponent<HitEffect_WildAxes>().SetOwner(owner); 
                    obj.GetComponent<HitEffect_WildAxes>().SetLeftOrRight(i == 0);
                    obj.GetComponent<HitEffect_WildAxes>().InitData(SkillData);
                    obj.GetComponent<HitEffect_WildAxes>().SetTarget(target);
                });
            }
        }
    }
}
