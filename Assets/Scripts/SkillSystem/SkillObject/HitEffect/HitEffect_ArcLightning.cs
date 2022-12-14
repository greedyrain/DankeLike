using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_ArcLightning : BaseSkillObject
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
                if (targetPos - ownerPos != Vector3.zero)
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
        isInitCompleted = false;
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            UniTask.Delay(500).ContinueWith(() =>
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