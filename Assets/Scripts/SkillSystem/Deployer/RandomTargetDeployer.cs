using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomTargetDeployer : SkillDeployer
{
    public LayerMask targetLayer;
    public override async void Generate()
    {
        transform.position = player.transform.position;
        //如果检测范围内有敌人
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, SkillData.range, targetLayer);
        if (colliders.Length > 0)
        {
            int index = Random.Range(0, colliders.Length);
            float x = colliders[index].transform.position.x;
            float y = colliders[index].transform.position.y;
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                    obj.transform.position = new Vector2(x, y);
                });
                await UniTask.Delay((int)(SkillData.actionInterval * 1000));
            }
        }
        
        else
        {
            float x = Random.Range(transform.position.x - SkillData.range,transform.position.x + SkillData.range);
            float y = Random.Range(transform.position.y - SkillData.range,transform.position.y + SkillData.range);
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                    obj.transform.position = new Vector2(x,y);
                });
                await UniTask.Delay((int)(SkillData.actionInterval * 1000));
            }
        }
    }
}
