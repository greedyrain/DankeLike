using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Button retryBtn;
    public Button backBtn;
    public override void Init()
    {
        retryBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.Reset();
        });
        
        backBtn.onClick.AddListener((() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            UIManager.Instance.HidePanel<JoyStickPanel>();
            Hide();
            UIManager.Instance.ShowPanel<MainMenuPanel>();
        }));
        
        
    }
}
