using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public int level;
    public ItemData itemData;
    public Sprite icon;
    public string itemName;
    public string description;

    public void InitData(int id, int level)
    {
        for (int i = 0; i < GameDataManager.Instance.Items.Count; i++)
        {
            if (GameDataManager.Instance.Items[i].ID == id && GameDataManager.Instance.Items[i].level == level)
            {
                itemData = GameDataManager.Instance.Items[i];
                break;
            }
        }
        icon = Resources.Load<Sprite>($"Sprites/ItemIcon/{itemData.iconName}");
        itemName = itemData.name;
        description = itemData.description;
    }
}
