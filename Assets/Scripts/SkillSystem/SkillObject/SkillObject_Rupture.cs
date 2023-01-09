using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Rupture : BaseSkillObject
{
    Enemy targetEnemy;
    Vector3 lastFramesPos;

    private async void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(async () =>
        {
            targetEnemy = GetComponentInParent<Enemy>();
            transform.SetParent(targetEnemy.center);
            lastFramesPos = transform.position;
            Action();
            float duration = owner.GetComponent<PlayerController>().CalculateDuration(SkillData.duration);
            await UniTask.Delay((int)(duration * 1000)).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });

    }

    public async void Action()
    {
        while (gameObject.activeSelf)
        {
            if (transform.position != lastFramesPos)
            {
                targetEnemy.GetHurt(owner.GetComponent<PlayerController>().CalculateDamage((int)((transform.position - lastFramesPos).magnitude * SkillData.damage)));
                lastFramesPos = transform.position;
                if (targetEnemy.isDead)
                {
                    targetEnemy = null;
                    PoolManager.Instance.PushObj(gameObject.name, gameObject);
                }
            }
            await UniTask.Delay(200);
        }
    }
}
