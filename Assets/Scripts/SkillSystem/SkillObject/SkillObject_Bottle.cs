using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Bottle : BaseSkillObject
{
    public float throwSpeed;
    private Vector2 destination;
    private bool isDestinationSet;

    private void OnEnable()
    {
        isDestinationSet = false;
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            destination = SetDestination();
        });
    }

    void Update()
    {
        UniTask.WaitUntil(() => initCompleted && isDestinationSet).ContinueWith(() =>
        {
            if (Vector2.Distance(transform.position,destination) > .5f)
            {
                transform.Translate(Vector2.right * Time.deltaTime * throwSpeed, Space.Self);
            }
            else
            {
                isDestinationSet = false;
                PoolManager.Instance.PushObj(SkillData.prefabName,gameObject);
                PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                {
                    obj.GetComponent<BaseSkillObject>().InitData(SkillData);
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                });
            }
        });
    }

    Vector2 SetDestination()
    {
        Vector2 destination = transform.position + transform.right.normalized * SkillData.range;
        isDestinationSet = true;
        return destination;
    }
}