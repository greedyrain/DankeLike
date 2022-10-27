using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Peojectile_Shotgun : BaseProjectile
{
    private void OnEnable()
    {
        UniTask.Delay(6000).ContinueWith(() => PoolManager.Instance.PushObj("Projectile_Shotgun",this.gameObject));
    }
}
