using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct EnemyTag : IComponentData { }

public struct EnemyMovementComponent : IComponentData
{
    public float3 TargetPosition;

    public quaternion TargetRotation;

    public float MoveSpeed;
}