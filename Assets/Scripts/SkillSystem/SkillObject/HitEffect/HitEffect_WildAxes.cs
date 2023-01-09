using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HitEffect_WildAxes : BaseSkillObject
{
    private PointF px1;
    private PointF px2;
    private PointF px3;
    private Vector3 targetPos;
    private Vector3 rotatePos1;
    private Vector3 rotatePos2;
    private bool ownerAndTargetSet;
    public bool isRight;
    public bool isPosSet;
    private bool isBack;

    private void OnEnable()
    {
        SetTargetAndOwner();
        UniTask.WaitUntil(() => initCompleted && ownerAndTargetSet).ContinueWith(() =>
        {
            transform.position = owner.position;
            if (target == null)
                PoolManager.Instance.PushObj(gameObject.name, gameObject);

            transform.position = owner.position;
            targetPos = target.position;
        });
    }

    private void Update()
    {
        UniTask.WaitUntil(() => initCompleted && isPosSet && ownerAndTargetSet).ContinueWith(() => { Fly(); });
    }


    public void Fly()
    {
        if (isRight)
            targetPos = new Vector3(target.position.x - 0.001f, targetPos.y, target.position.z - 0.001f);
        else
            targetPos = new Vector3(target.position.x + 0.001f, targetPos.y, target.position.z + 0.001f);
        transform.position = Vector3.Slerp(transform.position, targetPos, throwSpeed * Time.deltaTime);
    }

    public void SetLeftOrRight(bool isRight)
    {
        this.isRight = isRight;
        isPosSet = true;
    }

    void SetTargetAndOwner()
    {
        if (!isBack)
        {
            // owner = FindObjectOfType<PlayerController>().transform;
        }

        if (isBack)
        {
            owner = target;
            target = FindObjectOfType<PlayerController>().transform;
        }

        ownerAndTargetSet = true;
    }
}