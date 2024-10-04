using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1.0f;
    [SerializeField] private bool IsHittable = true;
    [SerializeField] private float HealthPoint = 3.0f;

    [SerializeField] private bool IsAttackable = true;
    [SerializeField] private float AttackValue = 1.0f;
    [SerializeField] private float AttackRange = 1.0f;

    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new UnitComponent { 
                Activate = true,
                HealthValue = authoring.HealthPoint
            });

            AddComponent(entity, new EnemyMovementComponent { 
                TargetPosition = float3.zero,
                TargetRotation = quaternion.identity,
                MoveSpeed = authoring.MoveSpeed
            });

            AddComponent(entity, new EnemyTag());

            if (authoring.IsHittable)
                AddComponent(entity, new HitComponent { IsHit = false });

            if (authoring.IsAttackable)
                AddComponent(entity, new AttackComponent { IsAttacking = false, Range = authoring.AttackRange, Damage = authoring.AttackValue });
        }
    }
}