using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BaseUnit : MonoBehaviour
{
    [HideInInspector] public int maxHP;
    [HideInInspector] public int HP;
    [HideInInspector] public int baseAtk;
    [HideInInspector] public int baseDef;
    [HideInInspector] public float moveSpeed;

    [Header("---------Total Data---------")] 
    public int totalAtk;
    public int totalDef;
    public float totalMoveSpeed;

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

    public virtual void GetHurt(int damage)
    {
        
    }
}