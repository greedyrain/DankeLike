using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BaseUnit : MonoBehaviour
{
    [Header("---------Base Data----------")]
    [HideInInspector] public int baseMaxHP;
    [HideInInspector] public int HP;
    [HideInInspector] public int baseArmor;
    [HideInInspector] public float baseMoveSpeed;

    [Header("---------Total Data---------")] 
    public float totalMight;
    public int totalArmor;
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