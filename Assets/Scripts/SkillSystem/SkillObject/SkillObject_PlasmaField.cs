using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_PlasmaField : BaseSkillObject
{
    private ParticleSystem partical;
    private CircleCollider2D coll;

    private float radius;
    public float rate;

    private bool isExtending;
    private bool isShrinking;
    
    private void Awake()
    {
        partical = GetComponent<ParticleSystem>();
        coll = GetComponent<CircleCollider2D>();
    }

    public void Extend()
    {
        radius = 0f;
        while (isExtending)
        {
            radius += Time.deltaTime * rate;
            coll.radius = radius;
            // partical.shape.radius = radius;
        
    }

    public void Shrink()
    {
        
    }
}
