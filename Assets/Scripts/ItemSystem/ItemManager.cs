using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有Item的增益效果，将这些增益效果附加到挂载该脚本的角色身上；
/// 提供一个事件，在获得物品时触发，让角色自身对自己的数值进行修改；
/// </summary>
public class ItemManager : MonoBehaviour
{
    BaseUnit owner;
    public List<BaseItem> ownedItemList = new List<BaseItem>();
    
    public int totalMaxHPEffect;
    public int totalArmorEffect;
    public float totalMoveSpeedEffect;
    public float totalMightEffect;
    public float totalDurationEffect;
    public int totalRecoveryEffect;
    public float totalCooldownEffect;
    public float totalMagnetEffect;
    public float totalExperienceEffect;
    public float totalThrowSpeedEffect;
    public float totalAreaEffect;

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
                totalMoveSpeedEffect -= ownedItemList[i].itemData.moveSpeedEffect;
                totalMightEffect -= ownedItemList[i].itemData.mightEffect;
                totalDurationEffect -= ownedItemList[i].itemData.durationEffect;
                totalRecoveryEffect -= ownedItemList[i].itemData.recoveryEffect;
                totalCooldownEffect -= ownedItemList[i].itemData.cooldownEffect;
                totalMagnetEffect -= ownedItemList[i].itemData.magnetEffect;
                totalExperienceEffect -= ownedItemList[i].itemData.experienceEffect;
                totalThrowSpeedEffect -= ownedItemList[i].itemData.throwSpeedEffect;
                totalAreaEffect -= ownedItemList[i].itemData.areaEffect;
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
        totalMoveSpeedEffect += item.itemData.moveSpeedEffect;
        totalMightEffect += item.itemData.mightEffect;
        totalDurationEffect += item.itemData.durationEffect;
        totalRecoveryEffect += item.itemData.recoveryEffect;
        totalCooldownEffect += item.itemData.cooldownEffect;
        totalMagnetEffect += item.itemData.magnetEffect;
        totalExperienceEffect += item.itemData.experienceEffect;
        totalThrowSpeedEffect += item.itemData.throwSpeedEffect;
        totalAreaEffect += item.itemData.areaEffect;
    }

    public void Activate()
    {
        owner.maxHPEffect = totalMaxHPEffect;
        owner.armorEffect = totalArmorEffect;
        owner.throwSpeedEffect = totalMoveSpeedEffect;
        owner.mightEffect = totalMightEffect;
        owner.durationEffect = totalDurationEffect;
        owner.recoveryEffect = totalRecoveryEffect;
        owner.cooldownEffect = totalCooldownEffect;
        owner.magnetEffect = totalMagnetEffect;
        owner.experienceEffect = totalExperienceEffect;
        owner.throwSpeedEffect = totalThrowSpeedEffect;
        owner.areaEffect = totalAreaEffect;
    }
}