using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseUnit
{
    public Vector2 direction;
    public GameObject weaponPos;
    
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
    
    public Weapon weapon;

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
    }


    protected override void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }

    public override void Move(Vector2 dir)
    {
        base.Move(dir);
        direction = dir;
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

    public void SetWeapon(int id)
    {
        string weaponName = "";
        for (int i = 0; i < GameDataManager.Instance.Weapons.Count; i++)
        {
            if (GameDataManager.Instance.Weapons[i].ID == id)
            {
                weaponName = GameDataManager.Instance.Weapons[i].name;
            }
        }
        weapon = Instantiate(Resources.Load<Weapon>($"Prefabs/Weapon/{weaponName}"));
        weapon.transform.SetParent(weaponPos.transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.ID = id;
    }
}
