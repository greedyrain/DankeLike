using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SkillObject_SunRay : BaseSkillObject
{
    private Collider coll;
    private Vector3 dir;
    private List<Collider> colls = new List<Collider>();
    private void OnEnable()
    {
        coll = GetComponent<Collider>();
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.localScale = new Vector3(1, 1,SkillData.range);
            transform.forward = owner.forward;
            Burn();
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                colls.Clear();
                PoolManager.Instance.PushObj(gameObject.name,gameObject);
            });
        });
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            colls.Add(col);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colls.Contains(other))
        {
            colls.Remove(other);
        }
    }

    public async void Burn()
    {
        float time = SkillData.duration;
        while (gameObject.activeSelf)
        {
            for (int i = 0; i < colls.Count; i++)
                colls[i].GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));

            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}
