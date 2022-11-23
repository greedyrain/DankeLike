using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_SplitEarth : BaseSkillObject
{
    private async void OnEnable()
    {
        await UniTask.WaitUntil(() => initCompleted).ContinueWith(async () =>
        {
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });

    }
}