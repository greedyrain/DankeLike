using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomDirectionDeployer : SkillDeployer
{
    public override void Generate()
    {
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.transform.position = player.transform.position;
                float angle = Random.Range(0, 360);
                obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
                {
                    PoolManager.Instance.PushObj(SkillData.prefabName, obj);
                });
            });
        }
    }
}
