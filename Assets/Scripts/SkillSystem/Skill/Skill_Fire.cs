using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Skill_ : BaseSkill
{
    protected override void Awake()
    {
        base.Awake();
        id = 1002;
    }

    public override void Generate()
    {
        
    }
    
    private void OnEnable()
    {
        owner.input.onMove += SetDirection;
    }

    private void OnDisable()
    {
        owner.input.onMove -= SetDirection;
    }

    void SetDirection(Vector2 dir)
    {
        transform.right = dir;
    }
}
