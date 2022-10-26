using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseUnit
{
    public int ID;
    [HideInInspector] public string userName;
    [HideInInspector] public int level;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int HP;
    [HideInInspector] public int atk;
    [HideInInspector] public int def;

    public PlayerData playerData;
    public PlayerInput input;
    private PlayerExperience playerExperience;
    public SkillManager playerSkillManager;

    public HealthBar healthBar;

    public override void Awake()
    {
        base.Awake();
        InitData();
        healthBar.ShowHP(maxHP,HP);
        playerExperience = GetComponent<PlayerExperience>();
        playerExperience.Init();
        playerSkillManager = GetComponent<SkillManager>(); 
    }

    public override void Start()
    {
        base.Start();
        input.EnableGamePlayInput();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        input.onMove += Move;
        input.onStopMove += StopMove;
        input.onSkill += TestAddSkill;
    }

    private void TestAddSkill()
    {
        
        // playerSkillManager.ObtainSkill();
    }

    protected override void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
        input.onSkill -= TestAddSkill;
    }

    public void GetHurt(int damage)
    {
        damage -= def;
        if (damage <= 0)
            damage = 0;
        HP -= damage;
        
        healthBar.ShowHP(maxHP,HP);
        if (HP <= 0)
        {
            isDead = true;
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("Dead");
    }

    public void InitData()
    {
        playerData = GameDataManager.Instance.PlayerData;
        moveSpeed = playerData.moveSpeed;
        userName = playerData.userName;
        level = playerData.level;
        maxHP = playerData.maxHP;
        HP = maxHP;
        atk = playerData.atk;
        def = playerData.def;
    }
}
