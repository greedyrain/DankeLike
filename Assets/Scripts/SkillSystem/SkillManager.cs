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
    private List<SkillDeployer> ownedSkill = new List<SkillDeployer>();
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
    void GenerateSkill(SkillDeployer deployer)
    {
        if (PrepareSkill(deployer))
        {
            deployer.Generate();
            deployer.SkillData.remainCD = deployer.SkillData.skillCD;
            CoolDownSkill(deployer);
        }
    }

    /// <summary>
    /// 判断技能是否满足释放条件
    /// </summary>
    /// <param name="deployer"></param>
    /// <returns></returns>
    bool PrepareSkill(SkillDeployer deployer)
    {
        if (deployer.SkillData.remainCD > 0)
            return false;
        return true;
    }

    /// <summary>
    /// 让技能进入冷却状态
    /// </summary>
    /// <param name="deployer"></param>
    async void CoolDownSkill(SkillDeployer deployer)
    {
        while (deployer.SkillData.remainCD > 0)
        {
            await UniTask.DelayFrame(1).ContinueWith(()=>deployer.SkillData.remainCD -= Time.deltaTime);
        }
    }

    /// <summary>
    /// 获得技能后，将技能存入玩家已获得技能列表，供外部调用
    /// </summary>
    /// <param name="id"></param>
    /// <param name="level"></param>
    public void ObtainSkill(int id,int level)
    {
        foreach (var data in skills)
        {
            if (data.ID == id && data.level == level)
            {
                SkillDeployer obj = Resources.Load<SkillDeployer>($"Prefabs/Skills/{data.skillName}");
                obj.SkillData = data;
                obj.player = player;
                ownedSkill.Add(obj);
            }
        }
    }

    private T CreatObject<T>(string className) where T : class
    {
        Type type = Type.GetType(className);
        return Activator.CreateInstance(type) as T;
    }
}