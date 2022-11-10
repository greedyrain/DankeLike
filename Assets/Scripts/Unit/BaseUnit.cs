using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseUnit : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    protected SpriteRenderer sr;

    public bool isDead;
    protected float moveSpeed;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
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
        if (dir.x > 0)
        {
            sr.flipX = true;
        }
        else if (dir.x < 0)
        {
            sr.flipX = false;
        }
    }

    public virtual void StopMove()
    {
        rb.velocity = Vector2.zero;
    }
}
