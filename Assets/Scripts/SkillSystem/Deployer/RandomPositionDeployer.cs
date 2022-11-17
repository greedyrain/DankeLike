using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomPositionDeployer : SkillDeployer
{
    public override async void Generate()
    {
        transform.position = player.transform.position;
        float x = Random.Range(player.transform.position.x-SkillData.range,player.transform.position.x+SkillData.range);
        float z = Random.Range(player.transform.position.z-SkillData.range,player.transform.position.z+SkillData.range);
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                obj.transform.position = new Vector3(x,0, z);
            });
            await UniTask.Delay((int)(SkillData.actionInterval * 1000));
        }
    }
}
