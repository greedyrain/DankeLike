using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Projectile_SerpentWard : BaseProjectile
{
    public int damage;
    public Transform target;

    private bool isInitCompleted;

    protected override void OnEnable()
    {
        base.OnEnable();
        
    }

    public override void Update()
    {
        UniTask.WaitUntil(() => isInitCompleted).ContinueWith(() =>
        {
            MoveToTarget(target);
        });
    }

    void MoveToTarget(Transform target)
    {
        Vector2.MoveTowards(transform.position, target.position, bulletSpeed * Time.deltaTime);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<Enemy>().GetHurt(damage);
        }
    }
}