using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DamagePopup : MonoBehaviour
{
    private TMP_Text text;

    private void OnEnable()
    {
        UniTask.Delay(1500).ContinueWith(() =>
        {
            PoolManager.Instance.PushObj(gameObject.name, gameObject);
        });
    }

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 0.1f * Time.deltaTime);
    }

    public void Setup(int damage,Transform pos)
    {
        transform.position = pos.position;
        text.text = damage.ToString();
    }
}
