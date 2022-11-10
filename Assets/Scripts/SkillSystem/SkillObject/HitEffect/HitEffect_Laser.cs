using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_Laser : MonoBehaviour
{
    public Transform target;
    public Transform owner;
    Vector2 targetPos;
    Vector2 ownerPos;

    public bool isInitCompleted;

    
    // Update is called once per frame
    async void Update()
    {
        await UniTask.WaitUntil(() => isInitCompleted).ContinueWith(async () =>
        {
            if (target == null)
            {
                UniTask.Delay(500).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
                return;
            }

            if (target.GetComponent<BaseUnit>().isDead)
            {
                target = null;
                return;
            }

            targetPos = target.position;
            transform.right = targetPos-ownerPos;
            ownerPos = owner.position;
            transform.position = ownerPos;

            float distance = (targetPos - ownerPos).magnitude;
            transform.localScale = new Vector3(distance, 1, 1);
            await UniTask.Delay(500).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });

    }

    private void OnEnable()
    {
        isInitCompleted = false;
    }

    public void Init(Transform target,Transform owner)
    {
        this.target = target;
        this.owner = owner;
        isInitCompleted = true;
    }

    
}
