using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonUnity<UIManager>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    public Transform canvas;

    public T GetPanel<T>() where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        return null;
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        GameObject panelObj = Instantiate(Resources.Load<GameObject>($"UI/{panelName}"), canvas);

        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName,panel);
        panel.Show();

        return panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].Hide(()=>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
}