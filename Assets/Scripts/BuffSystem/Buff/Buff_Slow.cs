using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Buff_Slow : BaseBuff
{
    public float proportion;

    public void InitData(int skillID, float proportion, float duration, BaseUnit owner)
    {
        this.owner = owner;
        this.skillID = skillID;
        this.proportion = proportion;
        this.duration = duration;
        remainTime = duration;
        actionType = BuffActionType.PERSISTENT;
        figureType = FigureType.PROPORTION;
    }

    public override void Action()
    {
        float effect = owner.moveSpeed * proportion;
        owner.totalMoveSpeed -= effect;
        UniTask.WaitUntil(() => remainTime <= 0).ContinueWith(() => owner.totalMoveSpeed += effect);
    }

}