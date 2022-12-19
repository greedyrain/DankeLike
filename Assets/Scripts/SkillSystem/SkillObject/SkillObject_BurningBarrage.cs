using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_BurningBarrage : BaseSkillObject
{
    Vector3 originPos;
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            originPos = transform.position;
        });
    }

    private void Update()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            Fly();
        });
    }

    public void Fly()
    {
        transform.Translate(Vector3.forward * SkillData.throwSpeed * Time.deltaTime);
        if ((transform.position - originPos).magnitude >= SkillData.range)
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHurt(SkillData.damage);
        }
    }
}
