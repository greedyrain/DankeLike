using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : SingletonUnity<GameManager>
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject plane;
    [SerializeField] private DropItem dropItem;
    [SerializeField] private GameCamera mainCamera;


    private PlayerController playerObj;
    private GameObject planeObj;

    public bool isPaused = true;

    void Start()
    {
        DontDestroyOnLoad(this);
        UIManager.Instance.ShowPanel<MainMenuPanel>();
    }

    public async Task StartGame(int levelID)
    {
        var sceneLoader = SceneManager.LoadSceneAsync("BattleScene");
        await UniTask.WaitUntil(() => sceneLoader.isDone).ContinueWith(() =>
        {
            planeObj = Instantiate(plane);
            planeObj.transform.position = Vector3.zero;
            UniTask.DelayFrame(2);
            playerObj = Instantiate(player);
            playerObj.transform.position = Vector3.zero + Vector3.up;
            float x, z;
            for (int i = 0; i < 5; i++)
            {
                x = Random.Range(0, 10);
                z = Random.Range(0, 10);
                DropItem drop = Instantiate(dropItem);
                drop.transform.position = new Vector3(x, 1, z);
                drop.Init(Random.Range(5, 10));
            }
            LevelManager.Instance.CumulateTimingOfGeneration();
            mainCamera = FindObjectOfType<GameCamera>();
            mainCamera.SetCameraFollowTarget(playerObj);
        });
    }

    public void EndGame()
    {
        SceneManager.LoadScene("BeginScene");
    }

    public void Reset()
    {
        FindObjectsOfType<BaseUnit>();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}