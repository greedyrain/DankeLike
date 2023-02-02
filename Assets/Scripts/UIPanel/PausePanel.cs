using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button quitBtn;
    public override void Init()
    {
        resumeBtn.onClick.AddListener(PressResumeButton);
        quitBtn.onClick.AddListener(PressQuitButton);
    }

    void PressResumeButton()
    {
        GameManager.Instance.ResumeGame();
        UIManager.Instance.ShowPanel<JoyStickPanel>();
        UIManager.Instance.HidePanel<PausePanel>();
    }

    void PressQuitButton()
    {
        GameManager.Instance.EndGame();
        UIManager.Instance.HidePanel<PausePanel>();
        UIManager.Instance.HidePanel<JoyStickPanel>();
        UIManager.Instance.HidePanel<GamePanel>();
        UIManager.Instance.ShowPanel<MainMenuPanel>();
    }
}
