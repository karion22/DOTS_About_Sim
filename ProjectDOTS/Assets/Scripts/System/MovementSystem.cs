using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct MovementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState inState)
    {
        inState.RequireForUpdate<MovementComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        var job = new MovementJob
        {
            dt = SystemAPI.Time.DeltaTime
        };

        job.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct MovementJob : IJobEntity
    {
        public float dt;

        public void Execute(ref LocalTransform refLocalTr, in MovementComponent inMovement)
        {
            float3 velocity = math.normalize(inMovement.TargetPosition - refLocalTr.Position) * inMovement.MoveSpeed;
            velocity.y = 0f;

            refLocalTr.Position += velocity * dt;
        }
    }
}