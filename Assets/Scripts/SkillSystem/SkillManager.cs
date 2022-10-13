using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    private List<SkillData> skills;
    private List<SkillData> ownedSkill;
    public PlayerController player;

    private void Awake()
    {
        skills = GameDataManager.Instance.SkillsData;
        player = GetComponent<PlayerController>();
    }

    public void GenerateSkill(SkillData data)
    {
        for (int i = 0; i < ownedSkill.Count; i++)
        {
            if (ownedSkill[i].ID == data.ID)
            {
                BaseSkill skill = PoolManager.Instance.GetObj("Prefabs/Skills", ownedSkill[i].prefabName).GetComponent<BaseSkill>();
                skill.InitData(player,data);
                skill.Generate();
            }
        }
    }

    public void ObtainSkill(int id,int level)
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
