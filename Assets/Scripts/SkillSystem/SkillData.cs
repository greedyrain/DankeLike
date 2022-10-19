using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillData
{
    //技能系列的ID
    public int ID;
    public int level;
    public List<string> impactTypes;
    public int count;
    public int damage;
    //技能名称，用于读取技能预制体
    public string skillName;
    //技能描述
    public string description;
    public float range;
    public float duration;
    public float skillCD;
    public float remainCD;
    //多数量的技能的释放间隔，以及DOT的伤害间隔
    public float interval;
    //技能生成的物体名称，用于读取技能产生的物体
    public string prefabName;
    //技能生成的物体的打击特效名称，用于读取技能产生的物体的打击特效
    public string hitEffectName;
}
