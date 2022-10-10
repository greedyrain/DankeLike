using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();
        target = GameObject.FindObjectOfType<PlayerController>();
    }
}
