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
    [SerializeField] private SphereCollider magneticArea;
    private float angle;
    
    [HideInInspector] public string userName;
    [HideInInspector] public int level;
    [HideInInspector] public int baseRecovery;

    [Header("Player Status")]
    public PlayerData playerData;

    public PlayerInput input;
    private PlayerExperience playerExperience;
    public SkillManager playerSkillManager;
    public ItemManager playerItemManager;

    public HealthBar healthBar;

    private bool controllable = true;

    public override void Awake()
    {
        base.Awake();
        InitData();
        playerExperience = GetComponent<PlayerExperience>();
        playerExperience.Init();
        playerSkillManager = GetComponent<SkillManager>();
        playerItemManager = GetComponent<ItemManager>();
        playerItemManager.onItemObtain += SetMagneticArea;
        playerItemManager.onItemObtain += SetPlayerSpeed;
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

    public void SetControllableStatus(bool status)
    {
        controllable = status;
    }

    public void SetMagneticArea()
    {
        magneticArea.radius = 1.5f + 1.5f * totalMagnetEffect;
    }

    public void SetPlayerSpeed()
    {
        totalMoveSpeed = baseMoveSpeed + baseMoveSpeed * totalSpeedEffect;
    }
}
