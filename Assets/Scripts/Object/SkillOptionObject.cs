using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillOptionObject : MonoBehaviour
{
    public Button button;
    public SkillData data;
    public Text skillName;

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            Debug.Log("-----+-+-++++-+--+-+"+UIManager.Instance.GetPanel<LevelUpPopupPanel>() == null);
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().UpdatePanelData(data);
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedSkill = this;
            Debug.Log(UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedSkill.data.skillName);
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