using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_ThunderStrike : BaseSkillObject
{
    public GameObject area;
    public int count;

    public Enemy targetEnemy;

    private async void OnEnable()
    {
        Debug.Log("OnEnable");
        await UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            Debug.Log("OnEnable2");
            count = SkillData.targetCount;
            targetEnemy = GetComponentInParent<Enemy>();
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
            area.transform.localScale = new Vector3(SkillData.radius * 2, area.transform.localScale.y, SkillData.radius * 2);
            Action();
        });
    }


    public async void Action()
    {
        while (count > 0)
        {
            Debug.Log(count);
            area.SetActive(true);
            Collider[] targets = Physics.OverlapSphere(transform.position, SkillData.radius, targetLayer);
            if (targets.Length > 0)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].GetComponent<Enemy>().GetHurt(owner.GetComponent<BaseUnit>().CalculateDamage(SkillData.damage));
                }
            }
            if (targetEnemy.isDead)
            {
                transform.SetParent(null);
            }
            count--;
            UniTask.Delay(200).ContinueWith(() => area.SetActive(false));
            if (count <= 0)
            {
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            }
            await UniTask.Delay((int)(SkillData.actionInterval * 1000));
        }
    }
}
