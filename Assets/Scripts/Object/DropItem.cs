using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DropItem : MonoBehaviour
{
    private int experience;
    
    public void Init(int drop)
    {
        experience = drop;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PoolManager.Instance.PushObj("DropItem",gameObject);
            other.transform.GetComponent<PlayerExperience>().ObtainDropItem(experience);
        }
    }
}
