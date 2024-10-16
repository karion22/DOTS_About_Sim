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
        float dt = SystemAPI.Time.DeltaTime * ECSUtility.GameTimeRatio;

        foreach(var (localTransform, movementInfo) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<CharacterMovementComponent>>())
        {
            float3 move = new float3(movementInfo.ValueRO.MoveDirection.x, 0, movementInfo.ValueRO.MoveDirection.y);
            localTransform.ValueRW.Position += move * dt * movementInfo.ValueRO.MoveSpeed;
            movementInfo.ValueRW.Position = localTransform.ValueRW.Position;

        }
    }
}
