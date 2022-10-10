using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillData
{
    public int ID;
    public string skillName;
    public string description;
    public float range;
    public float skillCD;
    public float remainCD;

    [Header("-----Prefab-----")]
    public string prefabName;
    public GameObject skillPrefab;

    [Header("-----Hit effect-----")]
    public string hitEffectName;
    public GameObject hitEffect;

    [Header("Target")]
    public string targetTag;
    public List<GameObject> targetList;
}
