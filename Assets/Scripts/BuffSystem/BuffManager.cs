using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public List<BaseBuff> persistentBuffList = new List<BaseBuff>();
    public List<BaseBuff> intermittentBuffList = new List<BaseBuff>();

    private void Update()
    {
        for (int i = 0; i < persistentBuffList.Count; i++)
        {
            persistentBuffList[i].Timer();
        }

        for (int i = 0; i < intermittentBuffList.Count; i++)
        {
            intermittentBuffList[i].Action();
        }
    }

    public void AddBuff(BaseBuff buff)
    {
        if (buff.actionType == BuffActionType.PERSISTENT)
        {
            bool hasDuplicates = false;
            // Check for duplicates, if has, replace it.
            for (int i = 0; i < persistentBuffList.Count; i++)
            {
                if (persistentBuffList[i].skillID == buff.skillID)
                {
                    Debug.Log("Duplicate");
                    hasDuplicates = true;
                    persistentBuffList[i].Refresh();
                }
            }

            if (!hasDuplicates)
            {
                persistentBuffList.Add(buff);
                buff.Action();
            }
        }
        else
        {
            bool hasDuplicates = false;
            // Check for duplicates, if has, replace it.
            for (int i = 0; i < intermittentBuffList.Count; i++)
            {
                if (intermittentBuffList[i].skillID == buff.skillID)
                {
                    Debug.Log("Duplicate");
                    hasDuplicates = true;
                    intermittentBuffList[i].Refresh();
                }
            }

            if (!hasDuplicates)
            {
                intermittentBuffList.Add(buff);
                buff.Action(); 
            }
        }
    }

    public void RemoveBuff(BaseBuff buff)
    {
        switch (buff.actionType)
        {
            case BuffActionType.PERSISTENT:
                if (persistentBuffList.Contains(buff))
                {
                    persistentBuffList.Remove(buff);
                }
                break;
            case BuffActionType.INTERMITTENT:
                if (intermittentBuffList.Contains(buff))
                {
                    intermittentBuffList.Remove(buff);
                }
                break;
        }
    }
}