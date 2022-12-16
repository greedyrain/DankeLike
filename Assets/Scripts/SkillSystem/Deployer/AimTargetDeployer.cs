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

    Collider[] targets;

    public override bool CheckForGenerate()
    {
        targets = Physics.OverlapSphere(transform.position, SkillData.range, targetLayer);
        if (targets.Length > 0)
        {
            return true;
        }
        return false;
    }

    public override async void Generate()
    {
        transform.position = player.transform.position;
        //如果检测范围内有敌人

        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                obj.transform.position = transform.position;
                obj.transform.forward = targets[0].transform.position - transform.position;
                //根据需要的效果选择距离最近的或者范围内随机的敌人
                switch (selectTargetType)
                {
                    //选择距离最近的敌人
                    case E_SelectTargetType.Nearst:
                        float minDistance = Vector3.Distance(transform.position, targets[0].transform.position);
                        for (int j = 1; j < targets.Length; j++)
                        {
                            if (Vector3.Distance(transform.position, targets[j].transform.position) < minDistance)
                            {
                                minDistance = Vector3.Distance(transform.position, targets[j].transform.position);
                                obj.transform.forward = targets[j].transform.position - transform.position;
                                obj.transform.position = targets[i].transform.position;
                                obj.GetComponent<BaseSkillObject>().SetTarget(targets[j].transform);
                            }
                        }
                        break;

                    //范围内随机敌人
                    case E_SelectTargetType.Random:
                        int index = Random.Range(0, targets.Length);
                        obj.transform.forward = targets[index].transform.position - transform.position;
                        obj.transform.position = targets[index].transform.position;
                        obj.GetComponent<BaseSkillObject>().SetTarget(targets[index].transform);
                        break;
                }
            });
            await UniTask.Delay((int)(SkillData.actionInterval * 1000));
        }


        //else
        //{
        //    float angle;
        //    for (int i = 0; i < SkillData.count; i++)
        //    {
        //        PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
        //        {
        //            obj.GetComponent<BaseSkillObject>().InitData(SkillData);
        //            obj.transform.position = transform.position;
        //            angle = Random.Range(0, 360);
        //            obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //        });
        //        await UniTask.Delay((int)(SkillData.actionInterval * 1000));
        //    }
        //}
    }
}