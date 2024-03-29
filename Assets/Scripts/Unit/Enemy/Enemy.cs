using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : BaseUnit
{
    public Transform center;
    public Transform damageDisplayPos;
    protected const float atkCD = 0.2f;
    protected float remainAtkCD = 0f;

    public int id;
    [HideInInspector] public string objName;
    [HideInInspector] public string description;

    [HideInInspector] public float patrolRadius;
    [HideInInspector] public float alertRadius;
    private float originalMoveSpeed;

    private int minDrop;
    private int maxDrop;

    [Header("Enemy Status")] public EnemyData enemyData;

    public int baseAtk;

    public LayerMask targetLayer;

    [HideInInspector] public Vector3 originPos;
    public Vector3 patrolPos;

    [HideInInspector] public EnemyStateMachine stateMachine;
    [HideInInspector] public PlayerController target;

    public override void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InitData();
        GetComponent<Collider>().enabled = true;
        isDead = false;
    }

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * totalMoveSpeed * Time.deltaTime);
    }

    public override void GetHurt(int damage)
    {
        if (isDead) return;
        base.GetHurt(damage);
        
        //Show the damage popup;
        DamagePopupManager.Instance.ShowDamage(damage, damageDisplayPos);

        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
            Drop();
            stateMachine.SwitchState(stateMachine.deadState);
        }

        Debug.Log($"Enemy takes {damage} damage. Remain HP is: {HP}.");
    }


    public virtual void Drop()
    {
        int drop = Random.Range(minDrop, maxDrop);
        PoolManager.Instance.GetObj("Prefabs", "DropItem", (obj) =>
        {
            obj.GetComponent<DropItem>().Init(drop);
            obj.transform.position = transform.position + Vector3.up * 0.25f;
            obj.transform.rotation = transform.rotation;
        });
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && remainAtkCD <= 0)
        {
            remainAtkCD = atkCD;
            collision.transform.GetComponent<PlayerController>().GetHurt(enemyData.atk);
        }

        remainAtkCD -= Time.deltaTime;
    }

    public void InitData()
    {
        for (int i = 0; i < GameDataManager.Instance.EnemiesData.Count; i++)
        {
            if (GameDataManager.Instance.EnemiesData[i].ID == id)
            {
                enemyData = GameDataManager.Instance.EnemiesData[i];
                break;
            }
        }

        basicMoveSpeed = enemyData.moveSpeed;
        totalMoveSpeed = basicMoveSpeed;
        originalMoveSpeed = enemyData.moveSpeed;
        objName = enemyData.name;
        description = enemyData.description;
        basicMaxHP = enemyData.maxHP;
        HP = enemyData.HP;
        baseAtk = enemyData.atk;
        basicArmor = enemyData.def;
        patrolRadius = enemyData.patrolRadius;
        alertRadius = enemyData.alertRadius;
        minDrop = enemyData.minDrop;
        maxDrop = enemyData.maxDrop;
    }

    public void SetMoveSpeed(float facotr)
    {
        basicMoveSpeed *= facotr;
    }

    public void ResetMoveSpeed()
    {
        basicMoveSpeed = originalMoveSpeed;
    }
}