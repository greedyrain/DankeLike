using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyDeadState : BaseEnemyState
{
    public override void OnEnter()
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.GetComponent<Collider2D>().enabled = false;
        UniTask.Delay(1500).ContinueWith(() =>
        {
            enemy.transform.localPosition = Vector3.zero;
            PoolManager.Instance.PushObj(enemy.transform.parent.name,enemy.transform.parent.gameObject);
        });
    }
}
