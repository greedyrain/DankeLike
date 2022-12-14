using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

[Serializable]
public class SkillData
{
    //技能系列的ID
    public int ID;
    //技能等级
    public int level;
    //技能数量（如燃烧瓶个数）
    public int count;
    //目标数量 (用于有攻击目标限制的技能，没有限制则为0)
    public int targetCount;
    //技能伤害
    public int damage;
    //技能名称，用于读取技能预制体
    public string skillName;
    //技能描述
    public string description;
    //释放范围
    public float range;
    //影响半径
    public float radius;
    //持续时间
    public float duration;
    //技能CD
    public float skillCD;
    //剩余CD时间
    public float remainCD;
    //物体的发射速度
    public float throwSpeed;
    //多数量的技能的释放间隔
    public float disposeInterval;
    //DOT的伤害间隔
    public float actionInterval;
    //技能生成的物体名称，用于读取技能产生的物体
    public string prefabName;
    //技能生成的物体的打击特效名称，用于读取技能产生的物体的打击特效
    public string hitEffectName;
}
