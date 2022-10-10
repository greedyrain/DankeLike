using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 管理和注册所有的状态，以及负责切换状态
/// </summary>
public class EnemyStateMachine : BaseStateMachine
{
    public EnemyPatrolState patrolState;
    public EnemyChaseState chaseState;

    private Enemy enemy;

    public void Init()
    {
        enemy = GetComponent<Enemy>();

        patrolState = new EnemyPatrolState();
        chaseState = new EnemyChaseState();

        patrolState.Init(enemy);
        chaseState.Init(enemy);

    }
}
