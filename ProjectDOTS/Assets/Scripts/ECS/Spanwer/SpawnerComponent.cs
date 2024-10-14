using Unity.Entities;
using Unity.Mathematics;

public struct SpawnerComponent : IComponentData
{
    // 생성할 유닛 프리팹
    public Entity Prefab;

    // 기본 생성 위치
    public float3 SpawnPosition;

    // 기본 회전 방향
    public quaternion SpawnRotation;

    // 생성 주기
    public float SpawnInterval;

    // 현재 유닛 개수
    public int CurrCount;

    // 목표 유닛 개수
    public int TargetCount;

    // 타이머
    public float Timer;

    // 생성 범위(거리)
    public float SpawnRange;
}
