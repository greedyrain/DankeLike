using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Buff_Stun : BaseBuff 
{
    public float proportion;

    public void InitData(int skillID, float duration, BaseUnit owner)
    {
        this.owner = owner;
        this.skillID = skillID;
        this.duration = duration;
        remainTime = duration;
        actionType = BuffActionType.PERSISTENT;
    }

    public override void Action()
    {
        if (owner.CompareTag("Enemy"))
        {
            IState previousState = new BaseEnemyState();
            Enemy enemy = owner.GetComponent<Enemy>();
            previousState = enemy.stateMachine.currentState;
            enemy.stateMachine.SwitchState(enemy.stateMachine.stunState);
            UniTask.WaitUntil(() => remainTime <= 0).ContinueWith(() => enemy.stateMachine.SwitchState(previousState));
        }

        if (owner.CompareTag("Player"))
        {
            //
        }
    }
}
