using Unity.Entities;
using Unity.Mathematics;

public struct CharacterComponent : IComponentData
{
    public float MoveSpeed;
}

public struct CharacterInput : IComponentData
{
    public float2 MoveDirection;
}
