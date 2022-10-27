using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class WeaponSelectPanel : BasePanel
{
    public Text optionDescription;
    public Button confirmBtn;
    private PlayerController player;
    public WeaponOptionObject currentSelectWeapon;
    public Transform content;
    private bool isInitCompleted;

    public override void Init()
    {
        player = FindObjectOfType<PlayerController>();
        confirmBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            UIManager.Instance.HidePanel<WeaponSelectPanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
            UIManager.Instance.ShowPanel<JoyStickPanel>();
            player.SetWeapon(currentSelectWeapon.data.ID);
        });
        isInitCompleted = true;
    }

    public override void Show()
    {
        base.Show();
        GenerateWeapon();
        UniTask.Delay(500).ContinueWith(() => Time.timeScale = 0);
    }

    public void GenerateWeapon()
    {
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < GameDataManager.Instance.Weapons.Count; i++)
            {
                if (!ids.Contains(GameDataManager.Instance.Weapons[i].ID))
                    ids.Add(GameDataManager.Instance.Weapons[i].ID);
            }
            
            for (int i = 0; i < ids.Count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs", "WeaponOptionObject", (obj) =>
                {
                    obj.GetComponent<WeaponOptionObject>().InitData(ids[i],1);
                    obj.transform.SetParent(content);
                });
            }
        });
    }

    public void UpdatePanelData(WeaponData data)
    {
        optionDescription.text = data.description;
    }
}