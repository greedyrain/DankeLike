using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSelectionObject : MonoBehaviour
{
    private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Text name;


    private PowerUpItemData data;
    [SerializeField] private List<Image> levelCheckMarks;

    private void OnEnable()
    {
        for (int i = 0; i < levelCheckMarks.Count; i++)
        {
            levelCheckMarks[i].gameObject.SetActive(false); 
        }
    }

    public void Initialize(int index)
    {
        data = GameDataManager.Instance.PowerUpData[index];
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { UIManager.Instance.GetPanel<PowerUpPanel>().UpdatePanel(data); });
        icon.sprite = Resources.Load<Sprite>($"Sprites/ItemIcons/{data.iconName}");
        name.text = data.name;

        if (data.currentLevel != 0)
        {
            for (int i = 0; i < data.currentLevel; i++)
            {
                levelCheckMarks[i].gameObject.SetActive(true);
            }
        }
    }
}