using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolEnemy : Enemy
{
    private Collider[] targetList;
    public override void Awake()
    {
        base.Awake();
        enemyData = GameDataManager.Instance.EnemiesData[0];
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stateMachine.Init();
        originPos = new Vector3(transform.position.x,0,transform.position.z);
        stateMachine.SwitchState(stateMachine.patrolState);
    }

    public  void Update()
    {
        Alert();
    }

    public void Alert()
    {
        if (Physics.OverlapSphere(transform.position, enemyData.alertRadius, targetLayer).Length > 0)
        {
            targetList = Physics.OverlapSphere(transform.position, enemyData.alertRadius, targetLayer);
            if (targetList.Length>0)
            {
                if (targetList.Length == 1)
                    target = targetList[0].GetComponent<PlayerController>();
                
                else
                    target = targetList[Random.Range(0,targetList.Length)].GetComponent<PlayerController>();
            }
            
            stateMachine.SwitchState(stateMachine.chaseState);
        }
        else if (target != null)
        {
            target = null;
            stateMachine.SwitchState(stateMachine.patrolState);
        }
    }
}
