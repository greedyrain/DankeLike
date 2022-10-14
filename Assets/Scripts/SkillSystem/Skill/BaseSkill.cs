using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 存储技能相关数据，实现技能施放后的处理
/// </summary>
public abstract class BaseSkill : MonoBehaviour
{
    public int id;
    public int level;
    public SkillData data;
    protected PlayerController owner;

    protected virtual void Awake()
    {
        // foreach (var item in GameDataManager.Instance.SkillsData)
        // {
        //     if (item.ID == id && item.level == level)
        //         data = item;
        // }
    }

    
    public abstract void Generate();

    public virtual void InitData(PlayerController owner,SkillData data)
    {
        this.owner = owner;
        this.data = data;
    }

}
