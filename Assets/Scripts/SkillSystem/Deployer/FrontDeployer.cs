using Cysharp.Threading.Tasks;
using UnityEngine;

public class FrontDeployer : SkillDeployer
{
    private float angle;
    private float averageAngle;
    public FrontDeployerType type;

    public override bool CheckForGenerate()
    {
        return true;
    }

    public override async void Generate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;

        switch (type)
        {
            case FrontDeployerType.FRONT:
                transform.forward = player.transform.forward;
                for (int i = 0; i < SkillData.count; i++)
                {
                    PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                    {
                        obj.transform.position = transform.position;
                        obj.transform.forward = player.transform.forward;
                        obj.GetComponent<BaseSkillObject>().InitData(SkillData,player);
                        obj.GetComponent<BaseSkillObject>().SetOwner(player.transform);
                    });
                    await UniTask.Delay((int)(SkillData.disposeInterval * 1000));
                }
                break;

            case FrontDeployerType.AVERAGEANGLE:
                angle = 60f;
                averageAngle = 60f / (SkillData.count - 1);
                for (int i = 0; i < SkillData.count; i++)
                {
                    PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
                    {
                        obj.transform.position = transform.position;
                        obj.transform.forward = Quaternion.AngleAxis((-angle / 2) + (i * averageAngle), Vector3.up) * transform.forward;
                        obj.GetComponent<BaseSkillObject>().InitData(SkillData,player);
                    });
                    await UniTask.Delay((int)(SkillData.disposeInterval * 1000));
                }
                break;
        }
    }
}