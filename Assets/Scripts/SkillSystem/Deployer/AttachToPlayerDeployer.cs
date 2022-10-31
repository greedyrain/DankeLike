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
            obj.GetComponent<BaseSkillObject>().InitData(SkillData);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.SetParent(player.transform);
        });
        await UniTask.Delay((int) (SkillData.actionInterval * 1000));
    }
}