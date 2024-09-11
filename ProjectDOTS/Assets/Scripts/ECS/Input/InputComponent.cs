using Unity.Entities;
using Unity.Mathematics;

public partial struct InputComponent : IComponentData
{
    public bool IsHitChanged;
    public float3 HitPoint;
}
