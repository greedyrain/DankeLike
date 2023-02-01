using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : SingletonUnity<GameManager>
{
    public Camera mainCamera;
    
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject plane;
    [SerializeField] private DropItem dropItem;

    void Start()
    {
        UIManager.Instance.ShowPanel<MainMenuPanel>();
    }

    public void StartGame(int levelID)
    {
        Instantiate(plane).transform.position = Vector3.zero;
        UniTask.DelayFrame(2);
        Instantiate(player).transform.position = Vector3.zero + Vector3.up;
        float x, z;
        for (int i = 0; i < 5; i++)
        {
            x = Random.Range(0, 10);
            z = Random.Range(0, 10);
            Instantiate(dropItem).transform.position = new Vector3(x, 1, z);
        }
    }

    public void Reset()
    {
        FindObjectsOfType<BaseUnit>();
    }
}
