using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuffData
{
    public int id;
    public string buffName;
    public string iconName;
    public float duration;
    public float remainTime;
    public float interval;
    public int damage;

    public event UnityAction onDispose;

    public void Dispose()
    {
        onDispose?.Invoke();
    }
}
