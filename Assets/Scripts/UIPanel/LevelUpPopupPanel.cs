using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopupPanel : BasePanel
{
    public Text optionDescription;
    public Button confirmBtn;
    private PlayerController player;
    private SkillManager playerSkillManager;
    public SkillOptionObject currentSelectedSkill;
    private bool isInitCompleted;

    public override void Init()
    {
        player = FindObjectOfType<PlayerController>();
        playerSkillManager = player.playerSkillManager;
        confirmBtn.onClick.AddListener(() =>
        {
            playerSkillManager.ObtainSkill(currentSelectedSkill.data);
            Time.timeScale = 1;
            UIManager.Instance.HidePanel<LevelUpPopupPanel>();
        });
        isInitCompleted = true;
    }

    public override void Show()
    {
        base.Show();
        GenerateRandomSkill();
        UniTask.Delay(500).ContinueWith(() => Time.timeScale = 0);
    }

    public void GenerateRandomSkill()
    {
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            List<int> ids = new List<int>();
            int index;
            int id;
            int level;
            for (int i = 0; i < GameDataManager.Instance.SkillsData.Count; i++)
            {
                if (!ids.Contains(GameDataManager.Instance.SkillsData[i].ID))
                    ids.Add(GameDataManager.Instance.SkillsData[i].ID);
            }
            


            for (int i = 0; i < 4; i++)
            {
                level = 1;
                index = Random.Range(0, ids.Count);
                id = ids[index];

                for (int j = 0; j < playerSkillManager.ownedSkill.Count; j++)
                {
                    if (playerSkillManager.ownedSkill[j].SkillData.ID == id)
                        level = playerSkillManager.ownedSkill[j].SkillData.level + 1;
                    if (level > 5)
                        level = 5;
                }
                
                ids.RemoveAt(index);
                PoolManager.Instance.GetObj("Prefabs", "SkillOptionObject", (obj) =>
                {
                    SkillOptionObject optObj = obj.GetComponent<SkillOptionObject>();
                    optObj.InitData(id, level);
                    optObj.transform.SetParent(transform.GetChild(0));
                });
            }
        });
    }

    public void UpdatePanelData(SkillData skill)
    {
        optionDescription.text = skill.description;
    }
}