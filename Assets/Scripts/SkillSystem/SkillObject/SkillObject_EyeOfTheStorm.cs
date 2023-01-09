using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_EyeOfTheStorm : BaseSkillObject
{
    public List<Transform> targetList = new List<Transform>();
    private Collider[] colls;
    private int targetCount;

    public Transform deployCenter;

    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 3, transform.localPosition.z);
            // SetTargetNum();
            SelectTarget();
            float duration = owner.GetComponent<PlayerController>().CalculateDuration(SkillData.duration);
            UniTask.Delay((int)(duration*1000)).ContinueWith(()=>{PoolManager.Instance.PushObj(gameObject.name, gameObject);});
        });
    }

    public void Lightning(Transform target)
    {
        PoolManager.Instance.GetObj("Prefabs/HitEffectObjects", SkillData.hitEffectName, (obj) =>
        {
            obj.GetComponent<HitEffect_Lightning>().InitData(SkillData);
            obj.GetComponent<HitEffect_Lightning>().Init(target, deployCenter);
        });
        target.GetComponent<Enemy>().GetHurt(owner.GetComponent<PlayerController>().CalculateDamage(SkillData.damage));
    }

    public async void SelectTarget()
    {
        int index;
        while (gameObject.activeSelf)
        {
            colls = Physics.OverlapSphere(transform.position, SkillData.range, targetLayer);
            if (colls.Length > 0)
            {
                for (int i = 0; i < SkillData.targetCount; i++)
                {
                    index = Random.Range(0, colls.Length);
                    targetList.Add(colls[index].transform);
                }
            }

            for (int i = 0; i < targetList.Count; i++)
            {
                Lightning(targetList[i]);
            }

            colls = null;
            targetList.Clear();
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}