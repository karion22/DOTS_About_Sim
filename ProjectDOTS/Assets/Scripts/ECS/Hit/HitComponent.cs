using Unity.Entities;
using Unity.Mathematics;

public struct HitComponent : IComponentData
{
    public bool IsHit;
    public float3 HitPosition;
}
