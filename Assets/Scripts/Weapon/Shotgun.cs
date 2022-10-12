using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject projectile;
    public Transform firePos;
    public int projectileCount;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Update()
    {
        if (remainCD > 0)
            remainCD -= Time.deltaTime;
        else
            Fire();
    }

    public void Init()
    {
    }

    public void Fire()
    {
        remainCD = atkCD;
        PoolManager.Instance.GetObj("Prefabs","Projectile_Shotgun", (obj) =>
        {
            obj.transform.position = firePos.position;
            obj.transform.rotation = transform.rotation;
            obj.GetComponent<Peojectile_Shotgun>().Init(this,transform.right);
        });
    }
}
