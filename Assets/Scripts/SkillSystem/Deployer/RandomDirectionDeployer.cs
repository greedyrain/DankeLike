using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomDirectionDeployer : SkillDeployer
{
    public override bool CheckForGenerate()
    {
        return true;
    }

    public override async void Generate()
    {
        transform.position = player.transform.position;
        float angle;
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                obj.transform.position = transform.position;
                angle = Random.Range(0, 360);
                obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            });
            await UniTask.Delay((int)(SkillData.actionInterval * 1000));
        }
    }
}
