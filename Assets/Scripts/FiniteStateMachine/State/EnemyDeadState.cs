using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyDeadState : BaseEnemyState
{
    public override async void OnEnter()
    {
        enemy.isDead = true;
        enemy.rb.velocity = Vector2.zero;
        enemy.GetComponent<Collider2D>().enabled = false;
        enemy.GetComponentInChildren<SpriteRenderer>().enabled = false;
        await UniTask.Delay(1000).ContinueWith(()=>
        {
            PoolManager.Instance.PushObj(enemy.transform.name, enemy.transform.gameObject);
            enemy.transform.localPosition = Vector3.zero;
        });
        
    }
}
