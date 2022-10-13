using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Events;

public class PoolManager : Singleton<PoolManager>
{
    //Dictionary (string)-->  PoolData --> List --> GameObject

    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    public GameObject poolObj;

    //Method of getting object by name from the Dictionary;
    public void GetObj(string path,string name, UnityAction<GameObject> callback)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            callback(poolDic[name].GetObj());
        else
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(path+"/"+name));
            obj.name = name;
            callback.Invoke(obj);
        }
    }
    
    public GameObject GetObj(string path,string name)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            return poolDic[name].GetObj();
        else
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(path+"/"+name));
            obj.name = name;
            return obj;
        }
    }

    //Method of pushing object into the Dictionary;
    public void PushObj(string name, GameObject obj)
    {
        if (poolObj == null)
            poolObj = new GameObject("Pool");

        if (poolDic.ContainsKey(name))
            poolDic[name].PushObj(obj);
        else
            poolDic.Add(name, new PoolData(obj, poolObj));
    }

    public void ClearPool()
    {
        poolDic.Clear();
        poolObj = null;
    }
}