using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SkillObject_Bottle : BaseSkillObject
{
    public float throwSpeed;
    private Vector2 destination;

    private void Awake()
    {
        // SetDestination();
    }

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * throwSpeed, Space.Self);
    }

    Vector2 SetDestination()
    {
        Vector2 destination = transform.position + transform.right.normalized * SkillData.range;
        Debug.Log(transform.position);
        Debug.Log(destination);
        return destination;
    }
}