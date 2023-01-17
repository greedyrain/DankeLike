using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DropItem : MonoBehaviour
{
    public int experience;
    
    public void Init(int drop)
    {
        experience = drop;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PoolManager.Instance.PushObj("DropItem",gameObject);
            other.transform.GetComponent<PlayerExperience>().ObtainDropItem(experience);
        }
        
        if (other.transform.CompareTag("MagneticArea"))
        {
            PoolManager.Instance.PushObj("DropItem",gameObject);
            other.transform.GetComponentInParent<PlayerExperience>().ObtainDropItem(experience);
        }
    }
}
