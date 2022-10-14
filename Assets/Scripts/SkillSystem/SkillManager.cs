using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 管理玩家身上挂在的技能，定期释放技能
/// </summary>
public class SkillManager : MonoBehaviour
{
    private List<SkillData> skills = new List<SkillData>();
    private List<SkillData> ownedSkill = new List<SkillData>();
    public PlayerController player;

    private void Awake()
    {
        skills = GameDataManager.Instance.SkillsData;
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (ownedSkill.Count > 0)
        {
            foreach (var skill in ownedSkill)
                GenerateSkill(skill);
        }
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="data"></param>
    void GenerateSkill(SkillData data)
    {
        if (PrepareSkill(data))
        {
            for (int i = 0; i < ownedSkill.Count; i++)
            {
                if (ownedSkill[i].ID == data.ID)
                {
                    int index = i;
                    //从池子对象池中获取，调用该技能的Generate方法；
                    PoolManager.Instance.GetObj("Prefabs/Skills", ownedSkill[i].prefabName, (obj) =>
                    {
                        BaseSkill skill = obj.GetComponent<BaseSkill>();
                        Debug.Log(ownedSkill[index].prefabName);
                        Debug.Log(obj == null);
                        skill.InitData(player, data);
                        skill.Generate();
                        ownedSkill[index].remainCD = ownedSkill[index].skillCD;
                        CoolDownSkill(ownedSkill[index]); 
                    });

                }
            }
        }
    }

    /// <summary>
    /// 判断技能是否满足释放条件
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    bool PrepareSkill(SkillData data)
    {
        if (data.remainCD > 0)
            return false;
        return true;
    }

    /// <summary>
    /// 让技能进入冷却状态
    /// </summary>
    /// <param name="data"></param>
    async void CoolDownSkill(SkillData data)
    {
        while (data.remainCD > 0)
        {
            await UniTask.DelayFrame(1).ContinueWith(()=>data.remainCD -= Time.deltaTime);
            
        }
    }

    /// <summary>
    /// 获得技能后，将技能存入玩家已获得技能列表，供外部调用
    /// </summary>
    /// <param name="id"></param>
    /// <param name="level"></param>
    public void ObtainSkill(int id, int level)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].ID == id && skills[i].level == level)
            {
                ownedSkill.Add(skills[i]);
            }
        }
    }
}