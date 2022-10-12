using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Peojectile_Shotgun : MonoBehaviour
{
    Weapon weapon;
    public float buletSpeed;
    private void OnEnable()
    {
        UniTask.Delay(6000).ContinueWith(() => PoolManager.Instance.PushObj("Projectile_Shotgun",this.gameObject));
    }

    void Update()
    {
        transform.Translate(Vector2.right * buletSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log(collision.transform.parent.name);
            collision.transform.GetComponent<Enemy>().GetHurt(weapon.damage);
        }
    }

    public void Init(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
