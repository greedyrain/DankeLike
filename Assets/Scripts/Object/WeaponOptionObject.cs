using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOptionObject : MonoBehaviour
{
    public Button button;
    public WeaponData data;
    public Text skillName;

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            UIManager.Instance.GetPanel<WeaponSelectPanel>().UpdatePanelData(data);
            UIManager.Instance.GetPanel<WeaponSelectPanel>().currentSelectWeapon = this;
            UIManager.Instance.GetPanel<WeaponSelectPanel>().confirmBtn.gameObject.SetActive(true);
        });
    }

    public void InitData(int id,int level)
    {
        foreach (var skill in GameDataManager.Instance.Weapons)
        {
            if (id == skill.ID && level == skill.level)
                data = skill;
        }

        skillName.text = data.name;
    }
}
