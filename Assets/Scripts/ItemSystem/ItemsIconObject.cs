using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ItemsIconObject : MonoBehaviour
{
    public int ID;
    private Image icon;

    public void Initialize(BaseItem baseItem)
    {
        icon = GetComponent<Image>();
        ID = baseItem.ID;
        icon.sprite = baseItem.icon;
    }
}
