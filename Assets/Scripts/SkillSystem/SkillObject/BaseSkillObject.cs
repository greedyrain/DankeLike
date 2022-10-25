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

    protected bool initCompleted = false;

    public void InitData(SkillData skillData)
    {
        this.skillData = skillData;
        initCompleted = true;
    }
}
