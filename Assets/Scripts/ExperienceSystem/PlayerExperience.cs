using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private PlayerController player;
    private List<ExperienceData> datas;

    private int maxLevel = 20;
    public int level;
    public int currentExp;
    public int maxExp;

    public void ObtainDropItem(int exp)
    {
        exp += (int)(exp * player.totalExperienceEffect);
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            if (level < maxLevel)
            {
                level++;
                UIManager.Instance.GetPanel<GamePanel>().UpdateLevelText(level);
                UIManager.Instance.ShowPanel<LevelUpPopupPanel>();
            }
            
            currentExp -= maxExp;
            maxExp = datas[level - 1].maxEXP;
            player.level = level;
        }
        UIManager.Instance.GetPanel<GamePanel>().UpdateExperience(currentExp,maxExp);
    }

    public void Init()
    {
        player = GetComponent<PlayerController>();
        datas = GameDataManager.Instance.ExperienceDatas;
        level = player.level;
        maxExp = datas[level - 1].maxEXP;
    }
}