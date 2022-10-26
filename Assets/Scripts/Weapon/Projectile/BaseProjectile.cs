using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected Weapon weapon;
    public float bulletSpeed;

    void Update()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime,Space.Self);
    }
    
    public virtual void Init(Weapon weapon,Vector2 dir)
    {
        this.weapon = weapon;
        transform.right = dir;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().GetHurt(weapon.damage);
        }
    }
}
