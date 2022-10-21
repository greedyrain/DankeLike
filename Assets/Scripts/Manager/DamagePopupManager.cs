using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DamagePopupManager : Singleton<DamagePopupManager>
{
    public void ShowDamage(int damage, Transform pos)
    {
        PoolManager.Instance.GetObj("Prefabs", "DamagePopup", (obj) =>
        {
            obj.GetComponent<DamagePopup>().Setup(damage, pos);
        });
    }
}