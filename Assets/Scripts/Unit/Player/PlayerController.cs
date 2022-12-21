using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : BaseUnit
{
    public Vector3 direction;
    public GameObject weaponPos;
    private float angle;
    
    [HideInInspector] public string userName;
    [HideInInspector] public int level;
    [HideInInspector] public int baseRecovery;

    [Header("Player Status")]
    public PlayerData playerData;
    
    public float totalMaxHPEffect;
    public float totalArmorEffect;
    public float totalSpeedEffect;
    public float totalMightEffect;
    public float totalDurationEffect;
    public float totalRecoveryEffect;
    public float totalCooldownEffect;
    public float totalMagnetEffect;

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
        damage -= totalArmor;
        if (damage <= 0)
            damage = 0;
        HP -= damage;
        
        healthBar.ShowHP(baseMaxHP,HP);
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
        userName = playerData.userName;
        level = playerData.level;
        HP = baseMaxHP;

        baseMoveSpeed = playerData.baseMoveSpeed;
        baseMaxHP = playerData.baseMaxHP;
        baseRecovery = playerData.baseRecovery;
        baseArmor = playerData.baseArmor;

        totalMoveSpeed = baseMoveSpeed;
        totalMight = playerData.baseMight;
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
