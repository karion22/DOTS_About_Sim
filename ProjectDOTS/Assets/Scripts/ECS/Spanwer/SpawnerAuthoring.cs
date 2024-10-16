using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    // ������ �� ������
    public GameObject Prefab;

    // ���� �ݰ�
    public float Radius;

    // ���� �ֱ�
    public float Interval;

    // ���� �� ����
    public int CurrCount;

    // ��ǥ �� ����
    public int TargetCount;

    // ���� ���� - �÷��̾ �߽����� �������� ���
    public float Range;

    public Entity TargetEntity;

    class SpanwerAuthoringBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);

            AddComponent(entity, new SpawnerComponent
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnRadius = authoring.Radius,
                SpawnRotation = authoring.transform.rotation,
                SpawnInterval = authoring.Interval,
                SpawnRange = authoring.Range,
                CurrCount = authoring.CurrCount,
                TargetCount = authoring.TargetCount
            });

            authoring.TargetEntity = entity;
        }
    }
}
