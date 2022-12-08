using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillCoolDownItem : MonoBehaviour
{
    public int ID;
    public Image baseImage;
    public Image icon;
    private SkillDeployer skill;

    private bool initCompleted;

    private void Update()
    {
        if (initCompleted)
        {
            icon.fillAmount = 1f - skill.SkillData.remainCD / skill.SkillData.skillCD;
        }
    }

    public void Init(SkillDeployer deployer)
    {
        initCompleted = false;
        ID = deployer.SkillData.ID;
        skill = deployer;
        baseImage.sprite = deployer.icon;
        icon.sprite = deployer.icon;
        baseImage.color = new Color(r: 48f, g: 48f, b: 48f, a: 255f);
        initCompleted = true;
    }
}
