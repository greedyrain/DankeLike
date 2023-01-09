using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttachToPlayerDeployer : SkillDeployer
{
    Collider[] targets;
    public LayerMask targetLayer;

    public override bool CheckForGenerate()
    {
        if (SkillData.range != 0)
        {
            targets = Physics.OverlapSphere(transform.position, SkillData.range, targetLayer);
            if (targets.Length > 0)
            {
                return true;
            }

            return false;
        }

        return true;
    }

    public override async void Generate()
    {
        player = FindObjectOfType<PlayerController>();
        transform.position = player.transform.position;
        PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
        {
            obj.GetComponent<BaseSkillObject>().InitData(SkillData, player);
            obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.transform.SetParent(player.transform);
        });
        await UniTask.Delay((int) (SkillData.actionInterval * 1000));
    }
}