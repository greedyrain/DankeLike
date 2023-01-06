using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_WaveForm : BaseSkillObject
{
    private Vector3 originPos;
    private bool moveFinish;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            owner.GetComponent<Rigidbody>().useGravity = false;
            owner.GetComponent<Collider>().isTrigger = true;
            originPos = transform.position;
        });
    }

    private void Update()
    {
        if (initCompleted && !CheckDistance())
            owner.transform.Translate(Vector3.forward * SkillData.throwSpeed * Time.deltaTime);
        if (CheckDistance())
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
            owner.GetComponent<PlayerController>().SetControllableStatus(true);
            owner.GetComponent<Collider>().isTrigger = false;
            owner.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private async void OnTriggerEnter(Collider other)
    {
        await UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            owner.GetComponent<PlayerController>().SetControllableStatus(false);
            if (initCompleted && other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
            }
        });
    }

    bool CheckDistance()
    {
        return (transform.position - originPos).magnitude > SkillData.range;
    }
}