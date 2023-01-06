using UnityEngine;

/// <summary>
/// 存储技能相关数据，实现技能施放后的处理
/// </summary>
public abstract class SkillDeployer : MonoBehaviour
{
    public Sprite icon;
    [HideInInspector] public PlayerController player;
    private SkillData skillData;
    public SkillData SkillData
    {
        get { return skillData; }
        set
        {
            skillData = value;
        }
    }

    public abstract void Generate();
    public abstract bool CheckForGenerate();
}
