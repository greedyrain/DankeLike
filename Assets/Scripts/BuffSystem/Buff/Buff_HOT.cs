using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Buff_HOT : BaseBuff
{
    public int heal;
    public float proportion;
    public float interval;
    public float remainInterval;


    public void InitData(int skillID, int heal, float proportion, float duration, float interval,
        FigureType figureType,BaseUnit owner)
    {
        this.owner = owner;
        this.skillID = skillID;
        this.heal = heal;
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

        if (remainInterval <= 0)
        {
            switch (figureType)
            {
                case FigureType.PROPORTION:
                    owner.HP += (int) (owner.baseMaxHP * proportion);
                    remainInterval = interval;
                    break;

                case FigureType.CONSTANT:
                    owner.HP += heal;
                    remainInterval = interval;
                    break;
            }
        }
    }
}