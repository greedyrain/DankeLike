using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillObject : MonoBehaviour
{
    protected PlayerController player;
    protected float duration;
    protected float throwSpeed;
    protected int damage;
    
    public LayerMask targetLayer;
    private SkillData skillData;
    public SkillData SkillData
    {
        get => skillData;
        set => skillData = value;
    }

    [HideInInspector] public Transform target;
    [HideInInspector] public Transform owner;

    protected bool initCompleted = false;

    public virtual void InitData(SkillData skillData, PlayerController player)
    {
        this.skillData = skillData;
        this.player = player;

        duration = player.CalculateDuration(SkillData.duration);
        throwSpeed = player.CalculateThrowSpeed(SkillData.throwSpeed);
        damage = player.CalculateDamage(SkillData.damage);

        initCompleted = true;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
    public void SetOwner(Transform owner)
    {
        this.owner = owner;
    }
}
