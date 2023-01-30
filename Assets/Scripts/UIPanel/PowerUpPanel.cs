using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPanel : BasePanel
{
    public Button backBtn;
    public override void Init()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<MainMenuPanel>();
            Hide();
        });
    }
}
