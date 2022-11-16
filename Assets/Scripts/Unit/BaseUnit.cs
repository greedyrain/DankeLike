using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseUnit : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator anim;
    private float angle;

    public bool isDead;
    protected float moveSpeed;
    protected float rotateSpeed;

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

    public virtual void Move(Vector2 dir)
    {
        angle = Vector3.Angle(Vector3.up, dir);
        angle = dir.x > 0 ? angle : -angle;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        rb.velocity = transform.forward * moveSpeed;
    }

    public virtual void StopMove()
    {
        
        rb.velocity = Vector3.zero;
    }
}
