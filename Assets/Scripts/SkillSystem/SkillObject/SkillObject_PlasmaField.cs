using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_PlasmaField : BaseSkillObject
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().GetHurt(SkillData.damage);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().GetHurt(SkillData.damage);
        }
    }

    public void Recycle()
    {
        PoolManager.Instance.PushObj(gameObject.name,gameObject);
    }
}
