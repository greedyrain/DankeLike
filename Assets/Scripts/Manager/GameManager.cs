using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowPanel<GamePanel>();
        UIManager.Instance.HidePanel<GamePanel>();
        UIManager.Instance.ShowPanel<LevelUpPopupPanel>();
    }
}
