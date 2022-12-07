using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RotateAroundDeployer : SkillDeployer 
{
    public LayerMask targetLayer;
    public override async void Generate()
    {
        transform.position = player.transform.position;
        //如果检测范围内有敌人
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, SkillData.range, targetLayer);
        await UniTask.WaitUntil(() => colliders.Length > 0).ContinueWith(async () =>
        {
            int index = Random.Range(0, colliders.Length);
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().SetTarget(colliders[index].transform);
                    obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                    // obj.transform.position = new Vector2(x, y);
                });
                await UniTask.Delay((int) (SkillData.actionInterval * 1000));
            }
        });
    }
}
