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
    public float count;
    public float duration;
    public bool isBoss;
    private string enemyPrefabName;

    /// <summary>
    /// 生成敌人
    /// </summary>
    /// <param name="id"></param>
    public async void GenerateEnemy()
    {
        if (isBoss)
        {
            Enemy[] enemys = GameObject.FindObjectsOfType<Enemy>();
            foreach (var enemy in enemys)
            {
                PoolManager.Instance.PushObj(enemy.gameObject.name,enemy.gameObject);
            }
        }
        
        for (int i = 0; i < GameDataManager.Instance.EnemiesData.Count; i++)
        {
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
                        float z = Random.Range(-9f, 9f);
                        obj.transform.position = new Vector3(x,0 ,z);
                    });
                    await UniTask.Delay((int) (duration * 1000 / count));
                }
            }
        }
    }
}