using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AttachToTargetDeployer : SkillDeployer
{
    public LayerMask targetLayer;
    int index;

    Collider[] targets;

    public override bool CheckForGenerate()
    {
        targets = Physics.OverlapSphere(player.transform.position, SkillData.range, targetLayer);
        if (targets.Length > 0)
        {
            return true;
        }
        return false;
    }

    public override async void Generate()
    {
        player = FindObjectOfType<PlayerController>();
        index = Random.Range(0, targets.Length);
        PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
        {
            obj.GetComponent<BaseSkillObject>().InitData(SkillData);
            obj.GetComponent<BaseSkillObject>().SetOwner(targets[index].transform);
            obj.transform.position = targets[index].transform.position;
            obj.transform.rotation = targets[index].transform.rotation;
            obj.transform.SetParent(targets[index].transform);
        });
        await UniTask.Delay((int)(SkillData.actionInterval * 1000));
    }

}
