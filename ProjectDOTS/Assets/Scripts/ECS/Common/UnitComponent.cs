using Unity.Entities;
using UnityEngine;

public struct UnitComponent : IComponentData
{
    // 사용 여부
    public bool Activate;

    // 체력
    public float HealthValue;
}
