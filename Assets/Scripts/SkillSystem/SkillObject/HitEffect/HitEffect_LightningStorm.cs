using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_LightningStorm : BaseSkillObject 
{
    Vector3 targetPos;
    Vector3 ownerPos;

    public bool isInitCompleted;
    void Update()
    {
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            if (target.gameObject.activeSelf && owner.gameObject.activeSelf)
            {
                targetPos = target.position;
                transform.position = targetPos + (Vector3.up * 10);
            }
        });
    }
    private void OnEnable()
    {
        isInitCompleted = false;
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            float duration = owner.GetComponent<PlayerController>().CalculateDuration(SkillData.duration);
            UniTask.Delay((int)(duration*1000)).ContinueWith(() =>
            {
                target = null;
                owner = null;
                isInitCompleted = false;
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
