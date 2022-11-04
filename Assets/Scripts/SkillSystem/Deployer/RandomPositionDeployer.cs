using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomPositionDeployer : SkillDeployer
{
    public override async void Generate()
    {
        transform.position = player.transform.position;
        float angle;
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                angle = Random.Range(0, 360);
                Vector3 excursion = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.forward*SkillData.range;
                // obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.position = excursion;
            });
            await UniTask.Delay((int)(SkillData.actionInterval * 1000));
        }
    }
}
