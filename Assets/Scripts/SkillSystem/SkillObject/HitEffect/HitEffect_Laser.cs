using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_Laser : MonoBehaviour
{

    SkillData skillData;
    public Transform target;
    public Transform owner;
    Vector2 targetPos;
    Vector2 ownerPos;

    public bool isInitCompleted;
    LineRenderer line;

    
    // Update is called once per frame
    async void Update()
    {
        await UniTask.WaitUntil(() => isInitCompleted).ContinueWith(async () =>
        {
            //if (target == null || owner == null)
            //{
            //    UniTask.Delay(500).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name, gameObject));
            //    return;
            //}

            //if (target.GetComponent<BaseUnit>().isDead)
            //{
            //    targetPos = target.position;
            //    transform.right = targetPos - ownerPos;
            //    target = null;
            //    return;
            //}

            //if (owner.GetComponent<BaseUnit>().isDead)
            //{
            //    ownerPos = owner.position;
            //    transform.position = ownerPos;
            //    owner = null;
            //    return;
            //}
            Debug.Log("456456456456456");
            if (target.gameObject.activeSelf && owner.gameObject.activeSelf)
            {
                targetPos = target.position;
                transform.right = targetPos - ownerPos;
                ownerPos = owner.position;
                transform.position = ownerPos;

                //target.GetComponent<Enemy>().GetHurt(skillData.damage);

                float distance = (targetPos - ownerPos).magnitude;
                transform.localScale = new Vector3(distance, 1, 1);
            }

            await UniTask.Delay(1000).ContinueWith(() =>
            {
                target = null;
                owner = null;
                isInitCompleted = false;
                PoolManager.Instance.PushObj(gameObject.name, gameObject);
            });
        });

    }

    private void OnEnable()
    {
        //line = GetComponent<LineRenderer>();
        //line.enabled = false;
        isInitCompleted = false;
    }

    public void Init(Transform target,Transform owner,SkillData skillData)
    {
        this.skillData = skillData;
        this.target = target;
        this.owner = owner;
        isInitCompleted = true;
        //line.enabled = true;
    }

    
}
