using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    private string enemyPrefabName;
    public void GenerateEnemy(int id)
    {
        for (int i = 0; i < GameDataManager.Instance.EnemiesData.Count; i++)
        {
            enemyPrefabName = "";
            if (GameDataManager.Instance.EnemiesData[i].ID == id)
                enemyPrefabName = GameDataManager.Instance.EnemiesData[i].name;

            if (!string.IsNullOrEmpty(enemyPrefabName))
            {
                PoolManager.Instance.GetObj("Prefabs/Enemy",enemyPrefabName, (obj) =>
                {
                    
                    float x = Random.Range(-15f, 15f);
                    float y = Random.Range(-15f, 15f);
                    obj.transform.position = new Vector2(x, y);
                });
            }
        }
    }

    private async void Start()
    {
        while (true)
        {
            await UniTask.Delay(5000).ContinueWith(() => GenerateEnemy(1001));
        }
    }
}
