using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttachToPlayerDeployer : SkillDeployer
{
    public override async void Generate()
    {
        transform.position = player.transform.position;
        PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.SetParent(player.transform);
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(SkillData.prefabName, obj);
            });
        });
        await UniTask.Delay((int) (SkillData.interval * 1000)).ContinueWith(() => { Debug.Log("Delaied"); });
    }
}