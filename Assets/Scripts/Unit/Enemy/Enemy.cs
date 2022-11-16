using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : BaseUnit
{
    protected const float atkCD = 0.2f;
    protected float remainAtkCD = 0f;

    public int id;
    [HideInInspector] public string objName;
    [HideInInspector] public string description;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int HP;
    [HideInInspector] public int atk;
    [HideInInspector] public int def;
    [HideInInspector] public float patrolRadius;
    [HideInInspector] public float alertRadius;

    private int minDrop;
    private int maxDrop;

    public EnemyData enemyData;

    public LayerMask targetLayer;

    [HideInInspector] public Vector2 originPos;
    public Vector2 patrolPos;

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
        GetComponent<Collider2D>().enabled = true;
        isDead = false;
    }


    public virtual void GetHurt(int damage)
    {
        if (isDead) return;
        
        HP -= damage;
        DamagePopupManager.Instance.ShowDamage(damage, transform);

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
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        });
    }


    private void OnCollisionStay2D(Collision2D collision)
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
        enemyData = GameDataManager.Instance.EnemiesData[id - 1];
        moveSpeed = enemyData.moveSpeed;
        objName = enemyData.name;
        description = enemyData.description;
        maxHP = enemyData.maxHP;
        HP = enemyData.HP;
        atk = enemyData.atk;
        def = enemyData.def;
        patrolRadius = enemyData.patrolRadius;
        alertRadius = enemyData.alertRadius;
        minDrop = enemyData.minDrop;
        maxDrop = enemyData.maxDrop;
    }
}