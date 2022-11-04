using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Laser : BaseSkillObject
{
    private LineRenderer line;
    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        UniTask.WaitUntil(() => initCompleted && target != null).ContinueWith(() =>
        {
            line.enabled = true;
            Active(owner,target);
            UniTask.Delay(300).ContinueWith(()=>{PoolManager.Instance.PushObj(gameObject.name, gameObject);});
        });
    }

    public void Active(Transform owner, Transform target)
    {
        transform.position = owner.position;
        transform.right = target.position - transform.position;
        float distance = (target.position - transform.position).magnitude;
        transform.localScale = new Vector3(distance, 1, 1);
        target.GetComponent<Enemy>().GetHurt(SkillData.damage);
    }
}