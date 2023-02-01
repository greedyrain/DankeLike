using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

public class PowerUpPanel : BasePanel
{
    [SerializeField] private PowerUpSelectionObject selectionObj;
    [SerializeField] private ScrollRect scrollView;
    
    [SerializeField] private Button backBtn;
    [SerializeField] private Button resetBtn;

    [SerializeField] private Text priceText;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Image icon;
    [SerializeField] private Toggle activeToggle;
    
    private int price;
    private string selectedEffectName;

    private PowerUpItemData currentSelectedItemData;

    public override void Show()
    {
        base.Show();
        for (int i = 0; i < GameDataManager.Instance.PowerUpData.Count; i++)
        {
            PowerUpSelectionObject selection = Instantiate(selectionObj);
            selection.Initialize(i);
            selection.transform.SetParent(scrollView.content);
        }
    }

    public override void Init()
    {
        backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<MainMenuPanel>();
            Hide();
        });
        
        buyBtn.onClick.AddListener(()=>Upgrade(selectedEffectName));
        resetBtn.onClick.AddListener(() =>
        {
            // show the warning panel;
        });
        activeToggle.onValueChanged.AddListener((isOn)=>currentSelectedItemData.isActive = isOn);
    }

    public void ResetPowerUpData()
    {
        GameDataManager.Instance.ResetPowerUpData();
        GameDataManager.Instance.ModifyMoney(GameDataManager.Instance.InGameData.totalUpgradeCost);
        GameDataManager.Instance.InGameData.totalUpgradeCost = 0;
        GameDataManager.Instance.SaveInGameData();
    }

    public void Upgrade(string name)
    {
        var powerUpList = GameDataManager.Instance.PowerUpData;
        foreach (var effect in powerUpList)
        {
            if (effect.name == name)
            {
                effect.currentLevel++;
                if (effect.currentLevel >= effect.maxLevel)
                    effect.currentLevel = effect.maxLevel;
            }
        }
        GameDataManager.Instance.ModifyMoney(-price);
        GameDataManager.Instance.ModifyTotalCost(price);
    }

    public void UpdatePanel(PowerUpItemData data)
    {
        currentSelectedItemData = data;
        selectedEffectName = data.name;
        buyBtn.gameObject.SetActive(true);
        priceText.gameObject.SetActive(true);
        description.text = data.description;
        icon.sprite = Resources.Load<Sprite>($"Sprites/ItemIcons/{data.iconName}");
        switch (data.currentLevel)
        {
            case 0:
                price = data.level1Price;
                break;
            case 1:
                price = data.level2Price;
                break;
            case 2:
                price = data.level3Price;
                break;
            case 3:
                price = data.level4Price;
                break;
            case 4: 
                price = data.level5Price;
                break;
            case 5:
                buyBtn.gameObject.SetActive(false);
                priceText.gameObject.SetActive(false);
                activeToggle.gameObject.SetActive(true);
                activeToggle.isOn = data.isActive;
                break;
        }
        priceText.text = price.ToString();
    }
}
