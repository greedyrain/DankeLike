using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleObject : MonoBehaviour
{
    public float throwSpeed;
    private Vector2 destination;

    void Update()
    {
        if (Vector2.Distance(transform.position,destination) > .5f)
            transform.Translate(transform.right * Time.deltaTime * throwSpeed,Space.Self);
        else
            PoolManager.Instance.PushObj(gameObject.name,gameObject);
    }

    public void Init(Vector2 destination)
    {
        transform.right = destination;
        Debug.Log(destination);
    }
}
