using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Weapon weapon;
    public float bulletSpeed;

    protected virtual void OnEnable()
    {
        UniTask.Delay(6000).ContinueWith(() => PoolManager.Instance.PushObj(gameObject.name,gameObject));
    }

    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime,Space.Self);
    }
    
    public virtual void Init(Weapon weapon,Vector2 dir)
    {
        this.weapon = weapon;
        transform.right = dir;
        bulletSpeed = weapon.weaponData.bulletSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().GetHurt(weapon.damage);
        }
    }
}
