using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class GenerateTiming
{
    public int enemyID;
    public int generateTiming;
    public int duration;
    public int count;
    public EnemyGenerator generator = new EnemyGenerator();
    private float remainTime;
    
    public async void Action()
    {
        while (remainTime > 0)
        {
            await UniTask.Delay((int)(duration*1000/count)).ContinueWith(() =>
            {
                generator.GenerateEnemy();
                remainTime -= duration* 1000/count ;
            });
        }
    }
}
