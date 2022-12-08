using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using EasyUI;
using UnityEngine;

public class SimpleUIFactory : PanelFactory
{
    public override UniTask<UIPanel> CreatePanelAsync(string name, CancellationToken token = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public override void RecyclePanel(UIPanel panel)
    {
        throw new System.NotImplementedException();
    }

    public override void Dispose()
    {
        throw new System.NotImplementedException();
    }
}
