using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 存储技能相关数据，实现技能施放后的处理
/// </summary>
public abstract class SkillDeployer : MonoBehaviour
{
    public Sprite icon;
    [HideInInspector] public PlayerController player;
    private SkillData skillData;
    public SkillData SkillData
    {
        get { return skillData; }
        set
        {
            skillData = value;
            // InitDeployer();
        }
    }

    private List<IImpactEffect> impactList;

    public abstract void Generate();
    public abstract bool CheckForGenerate();

    private void InitDeployer()
    {
        //影响
        for (int i = 0; i < skillData.impactTypes.Count; i++)
        {
            string className = string.Format($"{skillData.impactTypes[i]}Impact");
            Type type = Type.GetType(className);
            impactList[i] = Activator.CreateInstance(type) as IImpactEffect;
        }
    }
}
