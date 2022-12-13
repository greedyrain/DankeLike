using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillObject : MonoBehaviour
{
    public LayerMask targetLayer;
    private SkillData skillData;
    public SkillData SkillData
    {
        get => skillData;
        set => skillData = value;
    }

    [HideInInspector] public Transform target;
    [HideInInspector] public Transform owner;

    protected bool initCompleted = false;

    public virtual void InitData(SkillData skillData)
    {
        this.skillData = skillData;
        initCompleted = true;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
    public void SetOwner(Transform owner)
    {
        this.owner = owner;
    }
}
