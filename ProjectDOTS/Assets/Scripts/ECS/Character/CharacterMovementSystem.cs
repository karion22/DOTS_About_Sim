using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(EnemyAttackSystem))]
[BurstCompile]
public partial struct CharacterMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState inState)
    {
        inState.RequireForUpdate<LocalTransform>();
        inState.RequireForUpdate<CharacterMovementComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach(var (localTransform, movementInfo) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CharacterMovementComponent>>())
        {
            float3 move = new float3(movementInfo.ValueRO.MoveDirection.x, 0, movementInfo.ValueRO.MoveDirection.y);
            localTransform.ValueRW.Position += move * dt * movementInfo.ValueRO.MoveSpeed;
        }
    }
}
