using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public enum E_SelectTargetType
{
    Random,
    Nearst
}

public class AimTargetDeployer : SkillDeployer
{
    public E_SelectTargetType selectTargetType;
    public LayerMask targetLayer;

    public override async void Generate()
    {
        transform.position = player.transform.position;
        //如果检测范围内有敌人
        Collider[] colliders = Physics.OverlapSphere(transform.position, SkillData.range, targetLayer);
        if (colliders.Length > 0)
        {
            Debug.Log(colliders.Length);
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                    obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                    obj.transform.position = transform.position;
                    obj.transform.forward = colliders[0].transform.position - transform.position;
                    //根据需要的效果选择距离最近的或者范围内随机的敌人
                    switch (selectTargetType)
                    {
                        //选择距离最近的敌人
                        case E_SelectTargetType.Nearst:
                            float minDistance = Vector3.Distance(transform.position, colliders[0].transform.position);
                            for (int j = 1; j < colliders.Length; j++)
                            {
                                if (Vector3.Distance(transform.position, colliders[j].transform.position) < minDistance)
                                {
                                    minDistance = Vector3.Distance(transform.position, colliders[j].transform.position);
                                    obj.transform.forward = colliders[j].transform.position - transform.position;
                                    obj.transform.position = transform.position;
                                    obj.GetComponent<BaseSkillObject>().SetTarget(colliders[j].transform);
                                }
                            }
                            break;
                        
                        //范围内随机敌人
                        case E_SelectTargetType.Random:
                            int index = Random.Range(0, colliders.Length);
                            obj.transform.forward = colliders[index].transform.position - transform.position;
                            obj.transform.position = transform.position;
                            obj.GetComponent<BaseSkillObject>().SetTarget(colliders[index].transform);
                            break;
                    }
                });
                await UniTask.Delay((int)(SkillData.actionInterval * 1000));
            }
        }

        else
        {
            Debug.Log("No target-----------------");
            float angle;
            for (int i = 0; i < SkillData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                    obj.transform.position = transform.position;
                    angle = Random.Range(0, 360);
                    obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                });
                await UniTask.Delay((int)(SkillData.actionInterval * 1000));
            }
        }
    }
}