using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemOptionObject : MonoBehaviour
{
    public Sprite icon;
    public Button button;
    public BaseItem item;
    public ItemData data;
    public Text skillName;

    private void OnEnable()
    {
        button.onClick.AddListener(() =>
        {
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().UpdatePanelData(item.description);
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedItem = this;
            UIManager.Instance.GetPanel<LevelUpPopupPanel>().currentSelectedSkill = null;
        });
    }

    public void InitData(int id, int level)
    {
        foreach (var item in GameDataManager.Instance.Items)
        {
            if (id == item.ID && level == item.level)
                this.data = item;
        }
        item = new BaseItem();
        item.InitData(id, level);

        skillName.text = data.name;
    }
}
