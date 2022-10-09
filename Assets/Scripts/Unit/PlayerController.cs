using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseUnit
{
    [SerializeField] PlayerInput input;

    public override void Start()
    {
        base.Start();
        input.EnableGamePlayInput();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        input.onMove += Move;
        input.onStopMove += StopMove;
    }

    protected override void OnDisable()
    {
        input.onMove -= Move;
        input.onStopMove -= StopMove;
    }
}
