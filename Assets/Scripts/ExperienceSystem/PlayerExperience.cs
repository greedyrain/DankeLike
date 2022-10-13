using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private PlayerController player;
    private List<ExperienceData> datas;

    private int maxLevel = 6;
    public int level;
    public int currentExp;
    public int maxExp;

    public void ObtainDropItem(int exp)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            if (level < maxLevel)
                level++;
            
            currentExp -= maxExp;
            maxExp = datas[level - 1].maxEXP;
            player.level = level;
        }
    }

    public void Init()
    {
        player = GetComponent<PlayerController>();
        datas = GameDataManager.Instance.ExperienceDatas;
        level = player.level;
        maxExp = datas[level - 1].maxEXP;
    }
}