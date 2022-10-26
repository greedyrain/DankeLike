using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject bar;
    public void ShowHP(float maxHP,float currentHP)
    {
        float scale = currentHP / maxHP;
        if (scale < 0)
            scale = 0;
        bar.transform.localScale = new Vector3(scale,1,1);
    }
}
