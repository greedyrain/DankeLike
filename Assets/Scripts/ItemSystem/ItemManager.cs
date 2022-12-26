using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    BaseUnit owner;
    public List<BaseItem> ownedItemList = new List<BaseItem>();
    
    public float totalMaxHPEffect;
    public float totalArmorEffect;
    public float totalSpeedEffect;
    public float totalMightEffect;
    public float totalDurationEffect;
    public float totalRecoveryEffect;
    public float totalCooldownEffect;
    public float totalMagnetEffect;

    private void Awake()
    {
        owner = GetComponent<BaseUnit>();
    }

    public void AddItem(BaseItem item)
    {
        for (int i = 0; i < ownedItemList.Count; i++)
        {
            if (ownedItemList[i].itemData.ID == item.itemData.ID)
            {
                totalMaxHPEffect -= ownedItemList[i].itemData.maxHPEffectRate;
                totalArmorEffect -= ownedItemList[i].itemData.armorEffectRate;
                totalSpeedEffect -= ownedItemList[i].itemData.speedEffectRate;
                totalMightEffect -= ownedItemList[i].itemData.mightEffectRate;
                totalDurationEffect -= ownedItemList[i].itemData.durationEffectRate;
                totalRecoveryEffect -= ownedItemList[i].itemData.recoveryEffectRate;
                totalCooldownEffect -= ownedItemList[i].itemData.cooldownEffectRate;
                totalMagnetEffect -= ownedItemList[i].itemData.magnetEffectRate;
            }
            
            else
            {
                ownedItemList.Add(item);
            }
        }

        ActEffect(item);
        Activate();
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

    public void Activate()
    {
        owner.totalMaxHPEffect = totalMaxHPEffect;
        owner.totalArmorEffect = totalArmorEffect;
        owner.totalSpeedEffect = totalSpeedEffect;
        owner.totalMightEffect = totalMightEffect;
        owner.totalDurationEffect = totalDurationEffect;
        owner.totalRecoveryEffect = totalRecoveryEffect;
        owner.totalCooldownEffect = totalCooldownEffect;
        owner.totalMagnetEffect = totalMagnetEffect;
    }
}