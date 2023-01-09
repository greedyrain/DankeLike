using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    BaseUnit owner;
    public List<BaseItem> ownedItemList = new List<BaseItem>();
    
    public int totalMaxHPEffect;
    public int totalArmorEffect;
    public float totalSpeedEffect;
    public float totalMightEffect;
    public float totalDurationEffect;
    public int totalRecoveryEffect;
    public float totalCooldownEffect;
    public float totalMagnetEffect;
    public float totalExperienceEffect;

    public event Action onItemObtain;

    private void Awake()
    {
        owner = GetComponent<BaseUnit>();
    }

    public void AddItem(BaseItem item)
    {
        bool hasRepetition = false;
        for (int i = 0; i < ownedItemList.Count; i++)
        {
            if (ownedItemList[i].itemData.ID == item.itemData.ID)
            {
                totalMaxHPEffect -= ownedItemList[i].itemData.maxHPEffect;
                totalArmorEffect -= ownedItemList[i].itemData.armorEffect;
                totalSpeedEffect -= ownedItemList[i].itemData.speedEffect;
                totalMightEffect -= ownedItemList[i].itemData.mightEffect;
                totalDurationEffect -= ownedItemList[i].itemData.durationEffect;
                totalRecoveryEffect -= ownedItemList[i].itemData.recoveryEffect;
                totalCooldownEffect -= ownedItemList[i].itemData.cooldownEffect;
                totalMagnetEffect -= ownedItemList[i].itemData.magnetEffect;
                totalExperienceEffect -= ownedItemList[i].itemData.experienceEffect;
                ownedItemList[i] = item;
                hasRepetition = true;
                break;
            }
        }

        if (!hasRepetition)
        {
            ownedItemList.Add(item);
            UIManager.Instance.GetPanel<GamePanel>().InitItemIcon(item);
        }

        ActEffect(item);
        Activate();
        onItemObtain?.Invoke();
    }

    public void ActEffect(BaseItem item)
    {
        totalMaxHPEffect += item.itemData.maxHPEffect;
        totalArmorEffect += item.itemData.armorEffect;
        totalSpeedEffect += item.itemData.speedEffect;
        totalMightEffect += item.itemData.mightEffect;
        totalDurationEffect += item.itemData.durationEffect;
        totalRecoveryEffect += item.itemData.recoveryEffect;
        totalCooldownEffect += item.itemData.cooldownEffect;
        totalMagnetEffect += item.itemData.magnetEffect;
        totalExperienceEffect += item.itemData.experienceEffect;
    }

    public void Activate()
    {
        owner.maxHPEffect = totalMaxHPEffect;
        owner.armorEffect = totalArmorEffect;
        owner.speedEffect = totalSpeedEffect;
        owner.mightEffect = totalMightEffect;
        owner.durationEffect = totalDurationEffect;
        owner.recoveryEffect = totalRecoveryEffect;
        owner.cooldownEffect = totalCooldownEffect;
        owner.magnetEffect = totalMagnetEffect;
        owner.experienceEffect = totalExperienceEffect;
    }
}