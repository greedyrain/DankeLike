using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Buff_DOT : BaseBuff
{
    public int damage;
    public float proportion;
    public float interval;
    public float remainInterval;


    public void InitData(int skillID, int damage, float proportion, float duration, float interval,
        FigureType figureType,BaseUnit owner)
    {
        this.recipient = owner;
        this.skillID = skillID;
        this.damage = damage;
        this.interval = interval;
        this.proportion = proportion;
        this.duration = duration;
        remainTime = duration;
        actionType = BuffActionType.INTERMITTENT;
        this.figureType = figureType;
    }

    public override void Action()
    {
        remainInterval -= Time.deltaTime;
        remainTime -= Time.deltaTime;
        if (remainInterval <= 0 && remainTime > 0)
        {
            switch (figureType)
            {
                case FigureType.PROPORTION:
                    recipient.GetHurt((int) (recipient.baseMaxHP * proportion));
                    remainInterval = interval;
                    break;

                case FigureType.CONSTANT:
                    recipient.GetHurt(damage);
                    remainInterval = interval;
                    break;
            }
        }
    }
}