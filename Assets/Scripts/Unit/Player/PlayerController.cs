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
        basicMagneticRadius = magneticArea.radius;

        playerItemManager.onItemObtain += OnItemObtain;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Recover();
        UniTask.WaitUntil(() => UIManager.Instance.GetPanel<JoyStickPanel>()).ContinueWith(() =>
        {
            UIManager.Instance.GetPanel<JoyStickPanel>().OnDrag += Move;
            input.onGetHurt += () => { GetHurt(10); };
        });
    }

    public override void Start()
    {
        base.Start();
        input.EnableGamePlayInput();
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


    public void InitData()
    {
        playerData = GameDataManager.Instance.PlayerData;

        userName = playerData.userName;
        level = playerData.level;

        basicMaxHP = playerData.basicMaxHP;
        maxHP = basicMaxHP;
        HP = basicMaxHP;

        basicMoveSpeed = playerData.basicMoveSpeed;
        basicRecoveryEffect = playerData.basicRecovery;
        basicArmor = playerData.basicArmor;
        basicExperienceEffect = playerData.basicExperienceEffect;

        totalMoveSpeed = basicMoveSpeed;
        totalRecoveryEffect = basicRecoveryEffect;
    }

    private async void Recover()
    {
        while (!isDead)
        {
            await UniTask.Delay(1000).ContinueWith(() =>
            {
                HP += totalRecoveryEffect;
                if (HP >= (int) (maxHP + maxHP * totalMaxHPEffect))
                {
                    HP = (int) (maxHP + maxHP * totalMaxHPEffect);
                }
            });
        }
    }

    public void SetControllableStatus(bool status)
    {
        controllable = status;
    }

    #region Calculating Function

    //Calculate the damage of Skill Object
    public int CalculateDamage(int damage)
    {
        int totalDamage = damage + (int) (damage * totalMightEffect);
        return totalDamage;
    }

    //Calculate the duration of Skill Object
    public float CalculateDuration(float duration)
    {
        float resultDuration = duration + duration * totalDurationEffect;
        return resultDuration;
    }

    //Calculate the throwSpeed of Skill Object
    public float CalculateThrowSpeed(float throwSpeed)
    {
        float resultThrowSpeed = throwSpeed + (int) (throwSpeed * totalThrowSpeedEffect);
        return resultThrowSpeed;
    }

    //Calculate the cooldown of Skill
    public float CalculateCoolDown(float coolDown)
    {
        float resultCoolDown = coolDown - coolDown * totalCooldownEffect;
        return resultCoolDown;
    }

    //Calculate the area of Skill
    public float CalculateArea(float radius)
    {
        float resultRadius = radius + radius * totalAreaEffect;
        return resultRadius;
    }

    #endregion


    #region Functions of event
    //
    void OnItemObtain()
    {
        totalMaxHPEffect = basicMaxHPEffect + maxHPEffect;
        totalArmorEffect = basicArmorEffect + armorEffect;
        totalRecoveryEffect = basicRecoveryEffect + recoveryEffect;
        totalMoveSpeedEffect = basicMoveSpeedEffect + moveSpeedEffect;
        totalThrowSpeedEffect = basicThrowSpeedEffect + throwSpeedEffect;
        totalExperienceEffect = basicExperienceEffect + experienceEffect;
        totalMightEffect = basicMightEffect + mightEffect;
        totalDurationEffect = basicDurationEffect + durationEffect;
        totalAreaEffect = basicAreaEffect + areaEffect;
        totalCooldownEffect = basicCooldownEffect + cooldownEffect;
        totalMagnetEffect = basicMagneticEffect + magnetEffect;
        
        
        SetMagneticArea();
        SetPlayerMoveSpeed();
        SetMaxHP();
        SetArmor();
    }

    void SetMagneticArea()
    {
        magneticArea.radius = basicMagneticRadius + basicMagneticRadius * totalMagnetEffect;
    }

    void SetPlayerMoveSpeed()
    {
        totalMoveSpeed = basicMoveSpeed + basicMoveSpeed * totalMoveSpeedEffect;
    }

    void SetMaxHP()
    {
        maxHP = basicMaxHP + basicMaxHP * totalMaxHPEffect;
    }

    void SetArmor()
    {
        armor = basicArmor + basicArmor * totalArmorEffect;
    }

    #endregion
}