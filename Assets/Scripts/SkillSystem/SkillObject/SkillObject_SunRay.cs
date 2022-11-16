using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class SkillObject_SunRay : BaseSkillObject
{
    private Collider2D coll;
    private List<Collider2D> colls = new List<Collider2D>();
    private void OnEnable()
    {
        coll = GetComponent<Collider2D>();
        UIManager.Instance.GetPanel<JoyStickPanel>().OnDrag += SetSunRayDirection;
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.right = UIManager.Instance.GetPanel<JoyStickPanel>().direction;
            Burn();
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
            colls.Add(col);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (colls.Contains(other))
        {
            colls.Remove(other);
        }
    }

    public async void Burn()
    {
        Debug.Log("Radius is :"+SkillData.radius);
        float time = SkillData.duration;
        while (true)
        {
            for (int i = 0; i < colls.Count; i++)
                colls[i].GetComponent<Enemy>().GetHurt(SkillData.damage);

            await UniTask.Delay((int) (SkillData.actionInterval * 1000));
        }
    }
}
