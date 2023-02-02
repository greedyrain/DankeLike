using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{
    public Button startBtn;
    public Button powerUpBtn;
    public Button quitBtn;

    public override void Init()
    {
        startBtn.onClick.AddListener(async () =>
        {
            GameManager.Instance.isPaused = false;
            int levelIndex = GameDataManager.Instance.InGameData.currentLevel == null
                ? 1
                : GameDataManager.Instance.InGameData.currentLevel;
            await GameManager.Instance.StartGame(levelIndex);
            UIManager.Instance.ShowPanel<GamePanel>();
            UIManager.Instance.ShowPanel<LevelUpPopupPanel>();
            UIManager.Instance.HidePanel<MainMenuPanel>();
        });

        powerUpBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<PowerUpPanel>();
        });

        quitBtn.onClick.AddListener(() => { });
    }
}