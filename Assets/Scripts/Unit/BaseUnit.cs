using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BaseUnit : MonoBehaviour
{
    [Header("---------Basic Data----------")] 
    //original max health Point;
    [HideInInspector] public int basicMaxHP;
    //Current max health point;
    [HideInInspector] public int maxHP;
    //current health point;
    [HideInInspector] public int HP;
    //Original armor point;
    [HideInInspector] public int basicArmor;
    //Current armor point;
    [HideInInspector] public int armor;
    //Original move speed;
    [HideInInspector] public float basicMoveSpeed;
    //Current move speed;
    [HideInInspector] public float totalMoveSpeed;
    //original magnetic radius;    
    [HideInInspector] public float basicMagneticRadius;

 
    [HideInInspector] public int basicMaxHPEffect;
    [HideInInspector] public int basicRecoveryEffect;
    [HideInInspector] public int basicArmorEffect;
    [HideInInspector] public float basicMoveSpeedEffect;
    [HideInInspector] public float basicThrowSpeedEffect;
    [HideInInspector] public float basicExperienceEffect;
    [HideInInspector] public float basicMightEffect;
    [HideInInspector] public float basicDurationEffect;
    [HideInInspector] public float basicAreaEffect;
    [HideInInspector] public float basicCooldownEffect;
    [HideInInspector] public float basicMagneticEffect;


    [Header("---------Effect data from item---------")]
    public int maxHPEffect;
    public int armorEffect;
    public int recoveryEffect;
    public float moveSpeedEffect;
    public float throwSpeedEffect;
    public float experienceEffect;
    public float mightEffect;
    public float durationEffect;
    public float areaEffect;
    public float cooldownEffect;
    public float magnetEffect;

    [Header("---------Total Data---------")]
    public int totalMaxHPEffect;
    public int totalArmorEffect;
    public int totalRecoveryEffect;
    public float totalMoveSpeedEffect;
    public float totalThrowSpeedEffect;
    public float totalExperienceEffect;
    public float totalMightEffect;
    public float totalDurationEffect;
    public float totalAreaEffect;
    public float totalCooldownEffect;
    public float totalMagnetEffect;


    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;

    public bool isDead;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {
    }

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    protected virtual void OnDisable()
    {
    }

    public virtual void StopMove()
    {
        rb.velocity = Vector3.zero;
    }

    protected virtual void Dead()
    {
        Debug.Log("Dead");
    }

    public virtual void GetHurt(int damage)
    {
        damage -= armor;
        if (damage <= 0)
            damage = 0;
        HP -= damage;


        if (HP <= 0)
        {
            isDead = true;
            Dead();
        }
    }
}