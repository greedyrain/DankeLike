using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelManager : SingletonUnity<LevelManager>
{
    public int currentLevelID = 0;
    public LevelConfig currentLevel;
    public int gameTime;
    public List<LevelConfig> LevelConfigs;

    public override void Awake()
    {
        base.Awake();
        if (currentLevelID == 0)
        {
            SetLevel(1);
        }
    }

    private void Update()
    {
         currentLevel.Action(gameTime);
    }

    public void SetLevel(int id)
    {
        currentLevelID = id;
        foreach (var level in LevelConfigs)
        {
            if (level.ID == id)
                currentLevel = level;
            currentLevel.Init();
        }
    }
}
