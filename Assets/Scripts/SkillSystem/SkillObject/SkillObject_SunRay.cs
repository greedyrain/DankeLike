using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SkillObject_SunRay : BaseSkillObject
{
    private Collider2D coll;
    private void OnEnable()
    {
        coll = GetComponent<Collider2D>();
        UIManager.Instance.GetPanel<JoyStickPanel>().onDrag += SetSunRayDirection;
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.right = UIManager.Instance.GetPanel<JoyStickPanel>().direction;
            SwitchTrigger();
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name,gameObject);
            });
        });
    }

    public void SetSunRayDirection(Vector2 dir)
    {
        transform.right = dir;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>()?.GetHurt(SkillData.damage);
        }
    }

    public async void SwitchTrigger()
    {
        while (true)
        {
            coll.enabled = true;
            await UniTask.DelayFrame(1);
            coll.enabled = false;
            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        } 
    }
}
