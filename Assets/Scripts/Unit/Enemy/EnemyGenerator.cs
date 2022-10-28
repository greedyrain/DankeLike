using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyGenerator
{
    public int enemyID;
    public float generateTiming;
    public float duration;
    public float count;
    private string enemyPrefabName;

    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="id"></param>
    public async void GenerateEnemy()
    {
        for (int i = 0; i < GameDataManager.Instance.EnemiesData.Count; i++)
        {
            enemyPrefabName = "";
            if (GameDataManager.Instance.EnemiesData[i].ID == enemyID)
                enemyPrefabName = GameDataManager.Instance.EnemiesData[i].name;

            if (!string.IsNullOrEmpty(enemyPrefabName))
            {
                PoolManager.Instance.GetObj("Prefabs/Enemy", enemyPrefabName, (obj) =>
                {
                    float x = Random.Range(-9f, 9f);
                    float y = Random.Range(-9f, 9f);
                    obj.transform.position = new Vector2(x, y);
                });
            }

            await UniTask.Delay((int) (count / duration) * 1000);
        }
    }
}