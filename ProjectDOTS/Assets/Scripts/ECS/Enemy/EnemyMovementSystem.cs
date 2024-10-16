using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(CharacterMovementSystem))]
[BurstCompile]
public partial struct EnemyMovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState inState)
    {
        inState.RequireForUpdate<EnemyMovementComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        var targetQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag, LocalTransform>().Build();
        var targetLocalTrs = targetQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);

        var targetLocalTr = targetLocalTrs.Length > 0 ? targetLocalTrs[0] : default;

        var job = new MovementJob
        {
            m_TargetLocalTransform = targetLocalTr,
            m_TimeDelta = SystemAPI.Time.DeltaTime * ECSUtility.GameTimeRatio
        };

        job.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct MovementJob : IJobEntity
    {
        [ReadOnly] public LocalTransform m_TargetLocalTransform;
        public float m_TimeDelta;

        public void Execute(ref LocalTransform refLocalTr, in EnemyMovementComponent inMovement)
        {
            float3 direction = math.normalize(m_TargetLocalTransform.Position - refLocalTr.Position);
            float3 velocity = direction * inMovement.MoveSpeed;
            velocity.y = 0f;

            refLocalTr.Position += velocity * m_TimeDelta;
            refLocalTr.Rotation = quaternion.LookRotation(direction, math.up());
        }
    }
}