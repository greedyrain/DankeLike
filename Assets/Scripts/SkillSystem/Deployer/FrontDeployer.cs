using Cysharp.Threading.Tasks;
using UnityEngine;

public class FrontDeployer : SkillDeployer
{
    private float angle;
    private float averageAngle;
    public override async void Generate()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        angle = 60f;
        averageAngle = 60f / (SkillData.count - 1);
        for (int i = 0; i < SkillData.count; i++)
        {
            PoolManager.Instance.GetObj("Prefabs/SkillObjects", SkillData.prefabName, (obj) =>
            {
                obj.transform.position = transform.position;
                obj.transform.forward = Quaternion.AngleAxis((-angle / 2) + (i * averageAngle), Vector3.up)*transform.forward;
                obj.GetComponent<BaseSkillObject>().InitData(SkillData);
            });
            await UniTask.Delay((int) (SkillData.disposeInterval * 1000));
        }
    }
}