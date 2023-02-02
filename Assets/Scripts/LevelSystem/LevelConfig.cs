using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int ID;
    public List<EnemyGenerator> generators;

    public Dictionary<int, List<EnemyGenerator>> generateTimingDic = new Dictionary<int, List<EnemyGenerator>>();

    public void Activate(int nowTime)
    {
        foreach (KeyValuePair<int, List<EnemyGenerator>> kvp in generateTimingDic)
        {
            if (kvp.Key == nowTime && kvp.Value.Count > 0)
            {
                // bool check = false;
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    kvp.Value[i].GenerateEnemy();
                }
            }
        }
    }

    public void Init()
    {
        for (int i = 0; i < generators.Count; i++)
        {
            if (!generateTimingDic.ContainsKey((int) generators[i].generateTiming))
                generateTimingDic.Add((int) generators[i].generateTiming, new List<EnemyGenerator>() {generators[i]});
            else
                generateTimingDic[(int) generators[i].generateTiming].Add(generators[i]);
        }
    }
}