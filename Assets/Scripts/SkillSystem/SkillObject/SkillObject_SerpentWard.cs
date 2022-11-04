using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_SerpentWard : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name,gameObject));
        });
    }

    public void SetSerprentWard()
    {
        for (int i = 0; i < 10; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", "SerpentWard", (obj) =>
            {
                
            });
        }
    }
}
