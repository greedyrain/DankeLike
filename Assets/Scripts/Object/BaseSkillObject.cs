using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillObject : MonoBehaviour
{
    private SkillData skillData;
    public SkillData SkillData
    {
        get => skillData;
        set => skillData = value;
    }
}
