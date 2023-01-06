using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopupPanel : BasePanel
{
    public Text optionDescription;
    public Button confirmBtn;
    private PlayerController player;
    private SkillManager playerSkillManager;
    private ItemManager playerItemManager;
    public SkillOptionObject currentSelectedSkill;
    public ItemOptionObject currentSelectedItem;
    private bool isInitCompleted;

    public override void Init()
    {
        player = FindObjectOfType<PlayerController>();
        playerSkillManager = player.playerSkillManager;
        playerItemManager = player.playerItemManager;
        confirmBtn.onClick.AddListener(() =>
        {
            if (currentSelectedSkill == null && currentSelectedItem != null)
                playerItemManager.AddItem(currentSelectedItem.item);
            if (currentSelectedSkill != null && currentSelectedItem == null)
                playerSkillManager.ObtainSkill(currentSelectedSkill.data);

            Time.timeScale = 1;
            UIManager.Instance.HidePanel<LevelUpPopupPanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
            UIManager.Instance.ShowPanel<JoyStickPanel>();
        });
        isInitCompleted = true;
    }

    public override void Show()
    {
        base.Show();
        GenerateRandomSkillsOrItems();
        UniTask.Delay(300).ContinueWith(() => Time.timeScale = 0);
    }

    public async void GenerateRandomSkillsOrItems()
    {
        await UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            List<int> skillIDs = new List<int>();
            int skillIndex = 0;
            int skillID = 0;
            int skillLevel = 0;

            List<int> itemIDs = new List<int>();
            int itemIndex = 0;
            int itemID = 0;
            int itemLevel = 0;

            //if player have already got 6 skills, then will just pick the skill ids from which player already have.
            if (playerSkillManager.ownedSkill.Count == 6)
            {
                for (int i = 0; i < playerSkillManager.ownedSkill.Count; i++)
                {
                    if (playerSkillManager.ownedSkill[i].SkillData.level < 5)
                        skillIDs.Add(playerSkillManager.ownedSkill[i].SkillData.ID);
                }
            }
            //else, just pick skills ids from skillsData.
            else
            {
                for (int i = 0; i < GameDataManager.Instance.SkillsData.Count; i++)
                {
                    bool isThisMaxLevel = false;
                    foreach (var skill in playerSkillManager.ownedSkill)
                    {
                        if (GameDataManager.Instance.SkillsData[i].ID == skill.SkillData.ID &&
                            skill.SkillData.level == 5)
                            isThisMaxLevel = true;
                    }

                    //if the skill which its ID equals to the target ID, and it is max level, then skip this id;
                    if (isThisMaxLevel)
                        continue;

                    if (!skillIDs.Contains(GameDataManager.Instance.SkillsData[i].ID))
                        skillIDs.Add(GameDataManager.Instance.SkillsData[i].ID);
                }
            }


            //Generate Items IDs;
            if (playerItemManager.ownedItemList.Count == 6)
            {
                for (int i = 0; i < playerItemManager.ownedItemList.Count; i++)
                {
                    if (playerItemManager.ownedItemList[i].itemData.level < 5)
                        itemIDs.Add(playerItemManager.ownedItemList[i].itemData.ID);
                }
            }
            //else, just pick skills ids from skillsData.
            else
            {
                for (int i = 0; i < GameDataManager.Instance.Items.Count; i++)
                {
                    bool isThisMaxLevel = false;
                    foreach (var item in playerItemManager.ownedItemList)
                    {
                        if (GameDataManager.Instance.Items[i].ID == item.itemData.ID &&
                            item.itemData.level == 5)
                            isThisMaxLevel = true;
                    }

                    //if the skill which its ID equals to the target ID, and it is max level, then skip this id;
                    if (isThisMaxLevel)
                        continue;

                    if (!itemIDs.Contains(GameDataManager.Instance.Items[i].ID))
                        itemIDs.Add(GameDataManager.Instance.Items[i].ID);
                }
            }

            int type = 0;
            int count = Mathf.Min(4, skillIDs.Count + itemIDs.Count);
            for (int i = 0; i < count; i++)
            {
                if (skillIDs.Count > 0)
                {
                    skillLevel = 1;
                    skillIndex = Random.Range(0, skillIDs.Count);
                    skillID = skillIDs[skillIndex];
                }

                if (itemIDs.Count > 0)
                {
                    itemLevel = 1;
                    itemIndex = Random.Range(0, itemIDs.Count);
                    itemID = itemIDs[itemIndex];
                }

                //随机选择是出现物品还是出现技能，0为技能，1为物品；
                if (skillIDs.Count > 0 && itemIDs.Count > 0)
                    type = Random.Range(0, 2);
                else
                    type = skillIDs.Count > 0 ? 0 : 1;

                switch (type)
                {
                    case 0:
                        if (skillIDs.Count > 0)
                        {
                            for (int j = 0; j < playerSkillManager.ownedSkill.Count; j++)
                            {
                                if (playerSkillManager.ownedSkill[j].SkillData.ID == skillID)
                                    skillLevel = playerSkillManager.ownedSkill[j].SkillData.level + 1;
                            }

                            skillIDs.RemoveAt(skillIndex);
                            PoolManager.Instance.GetObj("Prefabs", "SkillOptionObject", (obj) =>
                            {
                                SkillOptionObject optObj = obj.GetComponent<SkillOptionObject>();
                                optObj.InitData(skillID, skillLevel);
                                optObj.transform.SetParent(transform.GetChild(0));
                            });
                        }
                        break;

                    case 1:
                        if (itemIDs.Count > 0)
                        {
                            for (int j = 0; j < playerItemManager.ownedItemList.Count; j++)
                            {
                                if (playerItemManager.ownedItemList[j].itemData.ID == itemID)
                                    itemLevel = playerItemManager.ownedItemList[j].itemData.level + 1;
                            }

                            itemIDs.RemoveAt(itemIndex);
                            PoolManager.Instance.GetObj("Prefabs", "ItemOptionObject", (obj) =>
                            {
                                ItemOptionObject optObj = obj.GetComponent<ItemOptionObject>();
                                optObj.InitData(itemID, itemLevel);
                                optObj.transform.SetParent(transform.GetChild(0));
                            });
                        }
                        break;
                }
            }
        });
    }

    public void UpdatePanelData(string description)
    {
        optionDescription.text = description;
    }
}