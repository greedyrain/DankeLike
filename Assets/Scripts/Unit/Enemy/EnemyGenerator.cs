using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyGenerator
{
    [Header("input the ID of the enemy you want to create.")]
    public int enemyID;
    [Header("input the time(second) when enemy you want to create enemy.")]
    public float generateTiming;
    [Header("input the count of enemies that you want to create.")]
    public float count;
    [Header("input the duration that you want to create all enemy completely.")]
    public float duration;
    private string enemyPrefabName;

    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="id"></param>
    public async void GenerateEnemy()
    {
        for (int i = 0; i < GameDataManager.Instance.EnemiesData.Count; i++)
        {
            Debug.Log((int) (duration * 1000/ count));
            enemyPrefabName = "";
            if (GameDataManager.Instance.EnemiesData[i].ID == enemyID)
                enemyPrefabName = GameDataManager.Instance.EnemiesData[i].name;
            
            if (!string.IsNullOrEmpty(enemyPrefabName))
            {
                for (int j = 0; j < count; j++)
                {
                    PoolManager.Instance.GetObj("Prefabs/Enemy", enemyPrefabName, (obj) =>
                    {
                        float x = Random.Range(-9f, 9f);
                        float y = Random.Range(-9f, 9f);
                        obj.transform.position = new Vector2(x, y);
                    }); 
                    await UniTask.Delay((int) (duration * 1000/ count));
                }
            }
        }

    }
}