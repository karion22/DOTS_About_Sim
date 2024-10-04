using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[UpdateAfter(typeof(CharacterInputSystem))]
public partial struct CharacterAttackSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var enemyQuery = SystemAPI.QueryBuilder().WithAll<EnemyTag, HitComponent, LocalTransform>().Build();
        var enemyLocalTr = enemyQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);

        // Job
        var attackJob = new AttackJob
        {
            ecb = ecb.AsParallelWriter(),
            enemyLocalTr = enemyLocalTr
        }.ScheduleParallel(inState.Dependency);

        attackJob.Complete();

        // 완료될 때까지 대기
        enemyLocalTr.Dispose();
        ecb.Dispose();
    }

    [BurstCompile]
    public partial struct AttackJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ecb;
        [ReadOnly] public NativeArray<LocalTransform> enemyLocalTr;

        public void Execute([EntityIndexInQuery] int inEntityIdx, Entity inEntity, in LocalTransform inLocalTr, in AttackComponent inAttackInfo, in HitComponent inHitInfo)
        {
            if (inAttackInfo.IsAttacking)
            {
                for (int i = 0, end = enemyLocalTr.Length; i < end; i++)
                {
                    var enemyPosition = enemyLocalTr[i].Position;
                    float dist = math.distance(inLocalTr.Position, enemyPosition);

                    if (dist < inAttackInfo.Range)
                    {
                        UnityEngine.Debug.Log("Attack Available");
                    }
                }

                ecb.SetComponent(inEntityIdx, inEntity, new AttackComponent { IsAttacking = false, Range = inAttackInfo.Range });                
            }
        }
    }
}
