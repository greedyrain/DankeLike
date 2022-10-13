using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillData
{
    public int ID;
    public int level;
    public string skillName;
    public string description;
    public float range;
    public float duration;
    public float skillCD;
    public float remainCD;
    public string prefabName;
    public string hitEffectName;
}
