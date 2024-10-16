using Unity.Entities;
using Unity.Mathematics;

public struct PlayerTag : IComponentData { }

public struct CharacterMovementComponent : IComponentData
{
    public float3 Position;

    public float2 MoveDirection;
    public float MoveSpeed;
}
