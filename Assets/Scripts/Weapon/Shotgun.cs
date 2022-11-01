using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shotgun : Weapon
{
    public Transform firePos;
    public float angle;
    private float averageAngle;

    protected  void OnEnable()
    {
        SetAngle();
        Fire();
    }

    public async void Fire()
    {
        while (true)
        {
            for (int i = 0; i < weaponData.count; i++)
            {
                PoolManager.Instance.GetObj("Prefabs", "Projectile_Shotgun", (obj) =>
                {
                    obj.transform.position = firePos.position;
                    obj.transform.rotation = transform.rotation;
                    obj.GetComponent<Peojectile_Shotgun>().Init(this, Quaternion.AngleAxis(-angle/2 + i * averageAngle,transform.forward) * transform.right);
                });
            }
            await UniTask.Delay((int) (weaponData.atkCD * 1000));
        }
    }

    public void SetAngle()
    {
        switch (weaponData.level)
        {
            case 1:
                angle = 15;
                break;
            case 2:
                angle = 20;
                break;
            case 3:
                angle = 30;
                break;
            case 4:
                angle = 45;
                break;
            case 5:
                angle = 60;
                break;
        }
        averageAngle = angle / (weaponData.count-1);
    }
}