using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class SkillObject_Avalanche : BaseSkillObject
{
    private Collider[] colls;
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.rotation =Quaternion.identity; 
            transform.localScale = new Vector3(SkillData.radius * 2, 0.05f, SkillData.radius * 2);
            Action();
            float duration = owner.GetComponent<PlayerController>().CalculateDuration(SkillData.duration);
            UniTask.Delay((int) (duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });
    }

    public async void Action()
    {
        while (gameObject.activeSelf)
        {
            colls = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
            if (colls.Length > 0)
            {
                foreach (var target in colls)
                {
                    Enemy enemy = target.GetComponent<Enemy>();
                    enemy.GetHurt(owner.GetComponent<PlayerController>().CalculateDamage(SkillData.damage));
                    Buff_Stun stun = new Buff_Stun();
                    stun.InitData(SkillData.ID,0.18f,enemy);
                    enemy.GetComponent<BuffManager>().AddBuff(stun);
                }
            }
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}