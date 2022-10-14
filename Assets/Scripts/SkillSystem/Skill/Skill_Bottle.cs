using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Skill_Bottle : BaseSkill,IThrow,IDamageOverTime
{
    private float throwSpeed = 1f;
    protected override void Awake()
    {
        base.Awake();
        id = 1002;
    }

    public override void Generate()
    {
        Vector2 destination;
        float angle = 360f / data.count;
        for (int i = 0; i < data.count; i++)
        {
            destination = (Quaternion.AngleAxis(angle * i, transform.forward) * transform.right).normalized * data.range;
            Throw(destination);
        }
    }
    
    // private void OnEnable()
    // {
    //     owner.input.onMove += SetDirection;
    // }
    //
    // private void OnDisable()
    // {
    //     owner.input.onMove -= SetDirection;
    // }
    //
    // void SetDirection(Vector2 dir)
    // {
    //     transform.right = dir;
    // }

    public override void InitData(PlayerController owner, SkillData data)
    {
        base.InitData(owner, data);
        transform.position = owner.transform.position;
        transform.rotation = owner.transform.rotation;
    }

    public void Throw(Vector2 destination)
    {
        transform.position = Vector2.MoveTowards(transform.position,destination,throwSpeed);
    }

    public async void DamageOverTime(GameObject target)
    {
        while (true)
        {
            await UniTask.Delay(200).ContinueWith(()=>target.GetComponent<Enemy>().HP -= data.damage);
        }
    }

    public void CancelDamageOverTime(GameObject target)
    {
        
    }
}
