using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomTargetDeployer : SkillDeployer
{
    public LayerMask targetLayer;
    Collider[] targets;

    public override bool CheckForGenerate()
    {
        targets = Physics.OverlapSphere(transform.position, SkillData.range, targetLayer);
        if (targets.Length > 0)
        {
            return true;
        }
        return false;
    }

    public override async void Generate()
    {
        transform.position = player.transform.position;
        //如果检测范围内有敌人
        await UniTask.WaitUntil(() => targets.Length > 0).ContinueWith(async () =>
        {
            int index = Random.Range(0, targets.Length);
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().SetTarget(targets[index].transform);
                    obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData,player);
                    // obj.transform.position = new Vector2(x, y);
                });
                await UniTask.Delay((int) (SkillData.actionInterval * 1000));
            }
        });
    }
}