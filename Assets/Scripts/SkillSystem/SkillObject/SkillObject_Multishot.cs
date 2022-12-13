using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Multishot : BaseSkillObject
{
    private void OnEnable()
    {
        UniTask.WaitUntil(() => initCompleted).ContinueWith(() =>
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            UniTask.Delay((int)(SkillData.duration * 1000)).ContinueWith(()=>PoolManager.Instance.PushObj(gameObject.name, gameObject));
        }); 
    }
    
    private void Update()
    {
        transform.Translate(Vector3.forward * SkillData.throwSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy") && initCompleted)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.GetHurt(SkillData.damage);
            //Slow
            // col.GetComponent<Enemy>().SetMoveSpeed(0.5f);
            // UniTask.Delay(5000).ContinueWith(() =>
            // {
            //     col.GetComponent<Enemy>().ResetMoveSpeed();
            // });
            Buff_Slow slow = new Buff_Slow();
            slow.InitData(SkillData.ID,0.5f,5,enemy);
            enemy.GetComponent<BuffManager>().AddBuff(slow);
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
            
        }
    } 
}
