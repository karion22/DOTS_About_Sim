using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    // 생성될 적 프리팹
    public GameObject Prefab;

    // 생성 반경
    public float Radius;

    // 생성 주기
    public float Interval;

    // 현재 적 개수
    public int CurrCount;

    // 목표 적 개수
    public int TargetCount;

    // 생성 범위 - 플레이어를 중심으로 원형으로 계산
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
