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
        startBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<GamePanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            UIManager.Instance.ShowPanel<LevelUpPopupPanel>();
        });

        powerUpBtn.onClick.AddListener(() =>
        {
            
        });

        quitBtn.onClick.AddListener(() => { });
    }
}