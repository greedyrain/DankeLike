using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : BaseUnit
{
    private float moveAngle;

    [HideInInspector] public string userName;
    [HideInInspector] public int level;

    [Header("------------Player Status------------")] 
    public PlayerData playerData;
    private bool controllable = true;


    [Header("------------player Component------------")]
    public PlayerInput input;
    private PlayerExperience playerExperience;
    public SkillManager playerSkillManager;
    public ItemManager playerItemManager;
    public HealthBar healthBar;
    [SerializeField] private SphereCollider magneticArea;


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
        playerItemManager.onItemObtain += SetPlayerRecovery;
        playerItemManager.onItemObtain += SetPlayerArmor;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Recover();
        UniTask.WaitUntil(() => UIManager.Instance.GetPanel<JoyStickPanel>()).ContinueWith(() =>
        {
            UIManager.Instance.GetPanel<JoyStickPanel>().OnDrag += Move;
            input.onGetHurt += ()=>{GetHurt(10);};
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
        
        Debug.Log($"Damage is : {damage}, current HP is {HP}.");
        
        if (HP <= 0)
        {
            isDead = true;
            Dead();
        }

        // healthBar.ShowHP(baseMaxHP, HP);
    }

    public virtual void Move(Vector2 dir)
    {
        if (controllable)
        {
            moveAngle = Vector3.Angle(Vector3.up, dir);
            moveAngle = dir.x > 0 ? moveAngle : -moveAngle;
            transform.rotation = Quaternion.Euler(0, moveAngle, 0);
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
        
        baseMaxHP = playerData.baseMaxHP;
        HP = baseMaxHP;

        baseMoveSpeed = playerData.baseMoveSpeed;
        baseRecovery = playerData.baseRecovery;
        baseArmor = playerData.baseArmor;

        experienceEffect = playerData.baseExperienceEffect;

        totalMoveSpeed = baseMoveSpeed;
        totalArmor = baseArmor;
        totalRecovery = baseRecovery;
    }

    private async void Recover()
    {
        while (!isDead)
        {
            await UniTask.Delay(1000).ContinueWith(() =>
            {
                HP += totalRecovery;
                if (HP >= (int) (baseMaxHP + baseMaxHP * maxHPEffect))
                {
                    HP = (int) (baseMaxHP + baseMaxHP * maxHPEffect);
                }
            });
        }
    }

    public void SetControllableStatus(bool status)
    {
        controllable = status;
    }

    void SetMagneticArea()
    {
        magneticArea.radius = 1.5f + 1.5f * magnetEffect;
    }

    void SetPlayerSpeed()
    {
        totalMoveSpeed = baseMoveSpeed + baseMoveSpeed * speedEffect;
    }

    void SetPlayerRecovery()
    {
        totalRecovery = baseRecovery + recoveryEffect;
    }

    void SetPlayerArmor()
    {
        totalArmor = baseArmor + armorEffect;
    }
    
    //Set the damage of Skill Object
    public int CalculateDamage(int damage)
    {
        int totalDamage = damage + (int)(damage*mightEffect);
        return totalDamage;
    }
    
    //Set the duration of Skill Object
    public float CalculateDuration(float duration)
    {
        float totalDuration = duration + duration*durationEffect;
        return totalDuration;
    }
    
    //Set the throwSpeed of Skill Object
    public float CalculateThrowSpeed(float throwSpeed)
    {
        float totalThrowSpeed = throwSpeed + (int)(throwSpeed*speedEffect);
        return totalThrowSpeed;
    }
}