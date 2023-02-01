using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance => instance;

    private PlayerData playerData;
    public PlayerData PlayerData => playerData; 
    
    
    private List<PowerUpItemData> powerUpData;
    public List<PowerUpItemData> PowerUpData => powerUpData;

    
    private List<EnemyData> enemiesData;
    public List<EnemyData> EnemiesData => enemiesData;

    
    private List<ItemData> items;
    public List<ItemData> Items => items;

    
    private List<ExperienceData> experienceDatas;
    public List<ExperienceData> ExperienceDatas => experienceDatas;

    
    private List<SkillData> skillsData;
    public List<SkillData> SkillsData => skillsData;

    private InGameData inGameData;
    public InGameData InGameData => inGameData;

    private GameDataManager()
    {
        enemiesData = JsonManager.Instance.LoadData<List<EnemyData>>("EnemyData");
        playerData = JsonManager.Instance.LoadData<PlayerData>("PlayerData");
        powerUpData = JsonManager.Instance.LoadData<List<PowerUpItemData>>("PowerUpData");
        experienceDatas = JsonManager.Instance.LoadData<List<ExperienceData>>("ExperienceData");
        skillsData = JsonManager.Instance.LoadData<List<SkillData>>("SkillsData");
        items = JsonManager.Instance.LoadData<List<ItemData>>("ItemsData");
        inGameData = JsonManager.Instance.LoadData<InGameData>("IngameData");
    }

    public void SaveInGameData()
    {
        JsonManager.Instance.SaveData(inGameData,"IngameData");
    }
    
    public void SavePowerUpData()
    {
        JsonManager.Instance.SaveData(powerUpData,"PowerUpData");
    }

    public void ModifyMoney(int value)
    {
        inGameData.money += value;
        SaveInGameData();
    }
    
    public void ModifyTotalCost(int value)
    {
        inGameData.totalUpgradeCost += value;
        SaveInGameData();
    }

    public void ResetPowerUpData()
    {
        foreach (var item in powerUpData)
        {
            item.currentLevel = 0;
        }
        SavePowerUpData();
    }
}
