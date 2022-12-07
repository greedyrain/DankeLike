using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillObject_Bedlam : BaseSkillObject
{
    private GameObject parent;
    private Collider[] colls;
    private int index;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            parent = new GameObject("Rotate Root");
            parent.transform.position = owner.transform.position;
            transform.position = owner.position + Vector3.up + Vector3.forward * SkillData.range;
            Debug.Log(parent.transform.position);
            Debug.Log(owner.position);
            transform.SetParent(parent.transform);
            CheckTarget();
            UniTask.Delay((int) (SkillData.duration * 1000))
                .ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });
    }

    private void Update()
    {
        Rotate();
    }

    public void Rotate()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            parent.transform.position = owner.transform.position;
            transform.RotateAround(owner.position, Vector3.up, 180 * Time.deltaTime);
        });
    }

    public async void CheckTarget()
    {
        while (gameObject.activeSelf)
        {
            colls = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                index = Random.Range(0, colls.Length);
                for (int i = 0; i < SkillData.targetCount; i++)
                {
                    PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
                    {
                        obj.GetComponent<HitEffect_Bedlam>().InitData(SkillData);
                        obj.GetComponent<HitEffect_Bedlam>().SetTarget(colls[index].transform);
                        obj.transform.position = transform.position;
                    });
                    await UniTask.Delay((int) (SkillData.disposeInterval * 1000));
                }
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }

    }
}