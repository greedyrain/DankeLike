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

    private bool levelActivated;

    public override void Awake()
    {
        base.Awake();
        if (currentLevelID == 0)
        {
            SetLevel(1);
        }
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

    public async void CumulateTimingOfGeneration()
    {
        GameManager.Instance.isPaused = false;
        while (!GameManager.Instance.isPaused)
        {
            currentLevel.Activate(gameTime);
            // await UniTask.Delay(1000).ContinueWith(() => gameTime++);
            await UniTask.Delay(1000);
        }
    }
}
