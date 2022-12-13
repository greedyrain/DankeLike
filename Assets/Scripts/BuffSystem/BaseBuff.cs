using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseBuff
{
    public int skillID;
    public BaseUnit owner;
    
    public string iconName;
    public float duration;
    public float remainTime;

    public BuffActionType actionType;
    public FigureType figureType;

    public abstract void Action();

    public void Refresh()
    {
        remainTime = duration;
    }

    public virtual void Timer()
    {
        remainTime -= Time.deltaTime;
        if (remainTime <= 0)
        {
            owner.GetComponent<BuffManager>().RemoveBuff(this);
        }
    }
}
