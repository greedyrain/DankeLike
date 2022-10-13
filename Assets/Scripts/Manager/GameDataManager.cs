using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance
    {
        get { return instance; }
    }

    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get { return playerData; }
    }

    private List<EnemyData> enemiesData;
    public List<EnemyData> EnemiesData
    {
        get { return enemiesData; }
    }

    private List<WeaponData> weapons;
    public List<WeaponData> Weapons
    {
        get { return weapons; }
    }

    private List<ExperienceData> experienceDatas;
    public List<ExperienceData> ExperienceDatas
    {
        get { return experienceDatas; }
    }

    private List<SkillData> skillsData;

    public List<SkillData> SkillsData
    {
        get { return skillsData; }
    }

    private GameDataManager()
    {
        weapons = JsonManager.Instance.LoadData<List<WeaponData>>("WeaponData");
        enemiesData = JsonManager.Instance.LoadData<List<EnemyData>>("EnemyData");
        playerData = JsonManager.Instance.LoadData<PlayerData>("PlayerData");
        experienceDatas = JsonManager.Instance.LoadData<List<ExperienceData>>("ExperienceData");
        skillsData = JsonManager.Instance.LoadData<List<SkillData>>("SkillsData");
    }
}
