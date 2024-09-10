using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct MovementComponent : IComponentData
{
    public float3 TargetPosition;

    public quaternion TargetRotation;

    public float MoveSpeed;
}
