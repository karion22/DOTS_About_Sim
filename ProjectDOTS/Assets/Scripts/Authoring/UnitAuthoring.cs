using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UnitAuthoring : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 1.0f;

    public class UnitBaker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new UnitComponent { 
                Activate = true
            });

            AddComponent(entity, new MovementComponent { 
                TargetPosition = float3.zero,
                TargetRotation = quaternion.identity,
                MoveSpeed = authoring.m_MoveSpeed
            });
        }
    }
}