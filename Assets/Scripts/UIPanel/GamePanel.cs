using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Text timeText;
    public float time;

    public List<SkillCoolDownItem> skillList;
    private int skillCount;
    public override void Init()
    {
        Show();
    }

    private void FixedUpdate()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        time += Time.fixedDeltaTime;
        LevelManager.Instance.gameTime = (int)time;
        timeText.text = System.TimeSpan.FromSeconds(time).ToString(format: @"mm\:ss\:ff");
    }

    public void InitSkillIcon(SkillDeployer deployer)
    {
        foreach (var skill in skillList)
        {
            if (skill.ID == deployer.SkillData.ID)
            {
                skill.Init(deployer);
                return;
            }
        }
        skillList[skillCount].Init(deployer);
        skillCount++;
    }
}
