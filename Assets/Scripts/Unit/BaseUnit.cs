using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseUnit : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;

    private bool isDead;
    public float moveSpeed;
    public float maxHP;
    public float HP;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        rb.gravityScale = 0;
    }

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    protected virtual void OnDisable()
    {

    }

    public virtual void Move(Vector2 dir)
    {
        rb.velocity = dir.normalized * moveSpeed;
    }

    public virtual void StopMove()
    {
        rb.velocity = Vector2.zero;
    }

    public virtual void GetHurt(int damage)
    {
        HP -= damage;
        if (HP <= 0 )
        {
            HP = 0;
            isDead = true;
        }
    }
}
