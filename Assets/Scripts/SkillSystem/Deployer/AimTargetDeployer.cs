using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class AimTargetDeployer : SkillDeployer
{
    public LayerMask targetLayer;

    public override async void Generate()
    {
        transform.position = player.transform.position;
        //如果检测范围内有敌人
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, SkillData.range, targetLayer);
        if (colliders.Length > 0)
        {
            Debug.Log(colliders.Length);
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().SkillData = SkillData;
                    obj.transform.position = transform.position;
                    obj.transform.right = colliders[0].transform.position - transform.position;
                    float minDistance = Vector2.Distance(transform.position, colliders[0].transform.position);
                    for (int i = 1; i < colliders.Length; i++)
                    {
                        if (Vector2.Distance(transform.position, colliders[i].transform.position) < minDistance)
                        {
                            minDistance = Vector2.Distance(transform.position, colliders[i].transform.position);
                            obj.transform.right = colliders[i].transform.position - transform.position;
                            obj.transform.position = transform.position;
                        }
                    }

                    UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
                    {
                        PoolManager.Instance.PushObj(SkillData.prefabName, obj);
                    });
                });
                await UniTask.Delay((int)(SkillData.interval * 1000));
            }
        }

        else
        {
            float angle;
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().SkillData = SkillData;
                    obj.transform.position = transform.position;
                    angle = Random.Range(0, 360);
                    obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
                    {
                        PoolManager.Instance.PushObj(SkillData.prefabName, obj);
                    });
                });
                await UniTask.Delay((int)(SkillData.interval * 1000));
            }
        }
    }
}