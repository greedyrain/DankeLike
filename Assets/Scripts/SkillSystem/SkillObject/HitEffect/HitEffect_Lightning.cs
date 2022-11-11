using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_Lightning : BaseSkillObject
{
    Vector2 targetPos;
    Vector2 ownerPos;

    public bool isInitCompleted;
    LineRenderer line;

    void Update()
    {
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            if (target.gameObject.activeSelf && owner.gameObject.activeSelf)
            {
                targetPos = target.position;
                transform.right = targetPos - ownerPos;
                ownerPos = owner.position;
                transform.position = ownerPos;
                float distance = (targetPos - ownerPos).magnitude;
                transform.localScale = new Vector3(distance, 1, 1);
            }
        });
    }

    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        isInitCompleted = false;
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            line.enabled = true;
            UniTask.Delay(300).ContinueWith(() =>
            {
                target = null;
                owner = null;
                isInitCompleted = false;
                line.enabled = false;
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
