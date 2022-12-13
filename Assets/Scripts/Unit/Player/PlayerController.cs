using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseUnit
{
    public Vector3 direction;
    public GameObject weaponPos;
    private float angle;
    
    [HideInInspector] public string userName;
    [HideInInspector] public int level;

    public PlayerData playerData;
    public PlayerInput input;
    private PlayerExperience playerExperience;
    public SkillManager playerSkillManager;

    public HealthBar healthBar;
    public Weapon weapon;

    private bool controllable = true;

    public override void Awake()
    {
        base.Awake();
        InitData();
        // healthBar.ShowHP(maxHP,HP);
        playerExperience = GetComponent<PlayerExperience>();
        playerExperience.Init();
        playerSkillManager = GetComponent<SkillManager>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        UniTask.WaitUntil(() => UIManager.Instance.GetPanel<JoyStickPanel>()).ContinueWith(() =>
        {
            UIManager.Instance.GetPanel<JoyStickPanel>().OnDrag += Move;
        });
    }

    public override void Start()
    {
        base.Start();
        input.EnableGamePlayInput();
    }

    public void GetHurt(int damage)
    {
        damage -= totalDef;
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

    public virtual void Move(Vector2 dir)
    {
        if (controllable)
        {
            angle = Vector3.Angle(Vector3.up, dir);
            angle = dir.x > 0 ? angle : -angle;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            transform.Translate(Vector3.forward * totalMoveSpeed * Time.deltaTime); 
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
        totalMoveSpeed = moveSpeed;
        userName = playerData.userName;
        level = playerData.level;
        maxHP = playerData.maxHP;
        HP = maxHP;
        baseAtk = playerData.atk;
        baseDef = playerData.def;
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

    public void SetControllableStatus(bool status)
    {
        controllable = status;
    }
}
