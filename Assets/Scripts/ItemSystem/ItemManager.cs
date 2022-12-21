using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<BaseItem> itemList;
    
    public float totalMaxHPEffect;
    public float totalArmorEffect;
    public float totalSpeedEffect;
    public float totalMightEffect;
    public float totalDurationEffect;
    public float totalRecoveryEffect;
    public float totalCooldownEffect;
    public float totalMagnetEffect;

    public void AddItem(BaseItem item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemData.ID == item.itemData.ID)
            {
                totalMaxHPEffect -= itemList[i].itemData.maxHPEffectRate;
                totalArmorEffect -= itemList[i].itemData.armorEffectRate;
                totalSpeedEffect -= itemList[i].itemData.speedEffectRate;
                totalMightEffect -= itemList[i].itemData.mightEffectRate;
                totalDurationEffect -= itemList[i].itemData.durationEffectRate;
                totalRecoveryEffect -= itemList[i].itemData.recoveryEffectRate;
                totalCooldownEffect -= itemList[i].itemData.cooldownEffectRate;
                totalMagnetEffect -= itemList[i].itemData.magnetEffectRate;
            }
            
            else
            {
                itemList.Add(item);
            }
        }

        ActEffect(item);
    }

    public void ActEffect(BaseItem item)
    {
        totalMaxHPEffect += item.itemData.maxHPEffectRate;
        totalArmorEffect += item.itemData.armorEffectRate;
        totalSpeedEffect += item.itemData.speedEffectRate;
        totalMightEffect += item.itemData.mightEffectRate;
        totalDurationEffect += item.itemData.durationEffectRate;
        totalRecoveryEffect += item.itemData.recoveryEffectRate;
        totalCooldownEffect += item.itemData.cooldownEffectRate;
        totalMagnetEffect += item.itemData.magnetEffectRate;
    }
}