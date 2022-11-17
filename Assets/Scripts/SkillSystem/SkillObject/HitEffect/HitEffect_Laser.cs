using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_Laser : BaseSkillObject
{
    Vector3 targetPos;
    Vector3 ownerPos;

    public bool isInitCompleted;
    // LineRenderer line;

    void Update()
    {
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            if (target.gameObject.activeSelf && owner.gameObject.activeSelf)
            {
                targetPos = target.position;
                transform.forward = targetPos - ownerPos;
                ownerPos = owner.position;
                transform.position = ownerPos;
                float distance = (targetPos - ownerPos).magnitude;
                transform.localScale = new Vector3(1, 1, distance);
            }
        });
    }

    private void OnEnable()
    {
        // line = GetComponent<LineRenderer>();
        // line.enabled = false;
        isInitCompleted = false;
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            // line.enabled = true;
            UniTask.Delay((int)(SkillData.duration*1000)).ContinueWith(() =>
            {
                target = null;
                owner = null;
                isInitCompleted = false;
                // line.enabled = false;
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });
    }

    public void Init(Transform target, Transform owner)
    {
        this.target = target;
        this.owner = owner;
        isInitCompleted = true;
    }
}