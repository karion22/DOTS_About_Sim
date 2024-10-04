using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CharacterAuthroing : MonoBehaviour
{
    [SerializeField] private float HealthValue = 10.0f;
    [SerializeField] private float MoveSpeed = 5.0f;

    [SerializeField] private float AttackRange = 5.0f;
    [SerializeField] private float AttackValue = 1.0f;

    public class CharacterBaker : Baker<CharacterAuthroing>
    {
        public override void Bake(CharacterAuthroing authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CharacterMovementComponent { MoveSpeed = authoring.MoveSpeed, MoveDirection = float2.zero });
            AddComponent(entity, new PlayerTag());
            AddComponent(entity, new AttackComponent { IsAttacking = false, Range = authoring.AttackRange, Damage = authoring.AttackValue });
            AddComponent(entity, new HitComponent { IsHit = false });
            AddComponent(entity, new UnitComponent { Activate = true, HealthValue = authoring.HealthValue });
        }
    }
}
