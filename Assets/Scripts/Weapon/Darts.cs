using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Darts : Weapon
{
    public Transform firePos;
    private float averageAngle;
    public LayerMask targetLayer;

    protected  void OnEnable()
    {
        Fire();
    }

    public async void Fire()
    {
        while (true)
        {
            Collider2D[] colliders =
                Physics2D.OverlapCircleAll(transform.position, weaponData.range, targetLayer);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < weaponData.count; i++)
                {
                    PoolManager.Instance.GetObj("Prefabs", "Projectile_Drats", (obj) =>
                    {
                        obj.transform.position = transform.position;
                        obj.transform.right = colliders[0].transform.position - transform.position;
                        float minDistance = Vector2.Distance(transform.position, colliders[0].transform.position);
                        for (int i = 1; i < colliders.Length; i++)
                        {
                            if (Vector2.Distance(transform.position, colliders[i].transform.position) < minDistance)
                            {
                                minDistance = Vector2.Distance(transform.position, colliders[i].transform.position);
                                obj.transform.right = colliders[i].transform.position - transform.position;
                            }
                        }
                        obj.GetComponent<BaseProjectile>().Init(this,obj.transform.right);
                    });
                    await UniTask.Delay((int) (weaponData.atkCD * 1000));
                }
            }

            else
            {
                float angle;
                for (int i = 0; i < weaponData.count; i++)
                {
                    PoolManager.Instance.GetObj("Prefabs", "Projectile_Drats", (obj) =>
                    {
                        // obj.GetComponent<BaseProjectile>().Init(this,transform.right);
                        obj.transform.position = transform.position;
                        angle = Random.Range(0, 360);
                        obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        obj.GetComponent<BaseProjectile>().Init(this,obj.transform.right);
                    });
                    await UniTask.Delay((int) (weaponData.atkCD * 1000));
                }
            }
        }
    }
}