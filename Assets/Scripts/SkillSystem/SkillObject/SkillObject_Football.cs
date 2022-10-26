using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Football : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            UniTask.Delay((int) (SkillData.duration * 1000)).ContinueWith(() =>
            {
                PoolManager.Instance.PushObj(gameObject.name,gameObject);
            });
        });
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * SkillData.throwSpeed, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //碰到墙反弹
        if (col.transform.CompareTag("Wall") || col.transform.CompareTag("Enemy"))
        {
            float rotationZ = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 inDirection = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));
            Vector2 normalDirection = col.contacts[0].normal;
            Vector2 outDirection = Vector2.Reflect(inDirection, normalDirection);
            rotationZ = Mathf.Atan2(outDirection.y, outDirection.x);
            transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);
        }
        if (col.transform.CompareTag("Enemy"))
        {
            Debug.Log(col.transform.parent.name);
            col.transform.GetComponent<Enemy>().GetHurt(SkillData.damage);
        }
    }
}
