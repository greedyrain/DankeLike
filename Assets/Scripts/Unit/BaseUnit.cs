using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseUnit : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;

    public bool isDead;
    protected float moveSpeed;

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
}
