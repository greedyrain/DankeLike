using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOptionObject : MonoBehaviour
{
    public Image icon;
    public Button button;
    public BaseItem item;
    public Text skillName;

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().UpdatePanelData(item.description);
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedItem = this;
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().confirmBtn.gameObject.SetActive(true);
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedSkill = null;
        });
    }

    public void InitData(int id, int level)
    {
        foreach (var item in GameDataManager.Instance.Items)
        {
            if (id == item.ID && level == item.level)
            {
                this.item = new BaseItem();
                this.item.InitData(id, level);
            }
        }

        icon = GetComponent<Image>();
        // icon.sprite = Resources.Load<Sprite>($"Sprites/ItemIcons/{item.itemData.name}");
        icon.sprite = item.icon;
        skillName.text = this.item.itemData.name;
    }
}
