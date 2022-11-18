using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_SerpentWard : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            SetSerprentWard();
            UniTask.Delay((int) (SkillData.duration * 1000))
                .ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
        });
    }

    public void SetSerprentWard()
    {
        Debug.Log(transform.position);
        for (int i = 0; i < 10; i++)
        {
            PoolManager.Instance.GetObj("Prefabs", "SerpentWard", (obj) =>
            {
                obj.transform.position = Quaternion.AngleAxis(36 * i, transform.up) * transform.forward + transform.position;
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
            });
        }
    }
}