using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AverageAngleDeployer : SkillDeployer 
{
    public  override async void Generate()
    {
        transform.position = player.transform.position;
        float angle = 360f / SkillData.count;
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.AngleAxis(angle * i, Vector3.forward);
            });
            await UniTask.Delay((int)(SkillData.interval * 1000));
        }
    }
}
