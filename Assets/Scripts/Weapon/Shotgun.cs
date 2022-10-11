using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public GameObject projectile;
    public int projectileCount;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Update()
    {
        if (remainCD > 0)
        {
            remainCD -= Time.deltaTime;
        }
        else
        {
            Fire();
        }
    }

    public void Init()
    {
        projectile = Resources.Load<GameObject>("Prefabs/Projectile_Shotgun");
    }

    public void Fire()
    {
        remainCD = atkCD;
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
