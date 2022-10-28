using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int ID;
    public List<EnemyGenerator> generators;

    public Dictionary<int, List<EnemyGenerator>> generateTimingDic = new Dictionary<int, List<EnemyGenerator>>();

    public void Action(int nowTime)
    {

    }

    public void Init()
    {

    }
}