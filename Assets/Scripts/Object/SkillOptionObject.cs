using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOptionObject : MonoBehaviour
{
    public Sprite icon;
    public Button button;
    public SkillData data;
    public Text skillName;

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().UpdatePanelData(data.description);
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedSkill = this;
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedItem = null;
        });
    }

    public void InitData(int id,int level)
    {
        foreach (var skill in GameDataManager.Instance.SkillsData)
        {
            if (id == skill.ID && level == skill.level)
                data = skill;
        }

        skillName.text = data.skillName;
    }
}