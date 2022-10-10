using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyState : IState
{
    protected Enemy enemy;

    public virtual void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnUpdate()
    {

    }


}
