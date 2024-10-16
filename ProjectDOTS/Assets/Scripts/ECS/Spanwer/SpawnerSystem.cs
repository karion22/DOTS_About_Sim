using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public partial class SpawnerSystem : SystemBase
{
    private Random m_Random;

    protected override void OnCreate()
    {
        RequireForUpdate<SpawnerComponent>();

        m_Random = new Random((uint)System.Environment.TickCount);
    }

    protected override void OnUpdate()
    {
        var spawner = SystemAPI.GetSingletonRW<SpawnerComponent>();

        float3 pos = float3.zero;
        foreach(var characterPos in SystemAPI.Query<RefRO<CharacterMovementComponent>>())
        {
            pos = characterPos.ValueRO.Position;
        }

        spawner.ValueRW.Timer += SystemAPI.Time.DeltaTime * ECSUtility.GameTimeRatio;
        if (spawner.ValueRO.Timer > spawner.ValueRO.SpawnInterval)
        {
            spawner.ValueRW.Timer = 0f;

            int diff = spawner.ValueRO.TargetCount - spawner.ValueRO.CurrCount;
            if (diff > 0)
            {
                float2 randPos = m_Random.NextFloat2(-spawner.ValueRO.SpawnRadius, spawner.ValueRO.SpawnRadius);
                float randAngle = m_Random.NextFloat(-180, 180);

                var entity = EntityManager.Instantiate(spawner.ValueRO.Prefab);
                EntityManager.SetComponentData(entity, new LocalTransform
                {
                    Position = new float3(pos.x + (randPos.x * math.cos(randAngle)), pos.y, pos.z + (randPos.y * math.sin(randAngle))),
                    Rotation = spawner.ValueRO.SpawnRotation,
                    Scale = 1.0f
                });

                spawner.ValueRW.CurrCount++;
            }
            else if (diff < 0)
            {
                spawner.ValueRW.CurrCount--;
                var query = SystemAPI.QueryBuilder().WithAll<UnitComponent>().Build();
                NativeArray<Entity> entities = query.ToEntityArray(Allocator.Temp);

                if (entities.Length > 0)
                {
                    EntityManager.DestroyEntity(entities[entities.Length - 1]);
                }

                entities.Dispose();
            }
        }
    }
}
/*
[BurstCompile]
public void OnUpdate(ref SystemState inState)
{
var dt = SystemAPI.Time.DeltaTime;

var spawner = SystemAPI.GetSingleton<SpawnerComponent>();

// 예전 버전과는 달리 1.2.4버전에서는 IComponentData 구조체의 값을 직접 수정할 수 없다.
foreach(var (spawner, entity) in SystemAPI.Query<RefRW<SpawnerComponent>>().WithEntityAccess())
{
    var spawnData = spawner.ValueRW;

    spawnData.Timer += dt;

    if(spawnData.Timer >= spawnData.SpawnInterval)
    {
        int diff = spawnData.TargetCount - spawnData.CurrCount;

        if(diff > 0)
        {
            NativeArray<Entity> spawnEntities = inState.EntityManager.Instantiate(spawnData.Prefab, diff, Allocator.Temp);

            foreach(var spawnedEntity in spawnEntities)
            {
                inState.EntityManager.SetComponentData(spawnedEntity, new LocalTransform
                {
                    Position = spawnData.SpawnPosition,
                    Rotation = spawnData.SpawnRotation,
                    Scale = 1f
                });
                inState.EntityManager.AddComponentData(spawnedEntity, new UnitComponent
                {
                });
                spawnData.CurrCount++;
            }

            spawnEntities.Dispose();
        }
        else if(diff < 0)
        {
            NativeArray<Entity> destoryEntities = m_EntityQuery.ToEntityArray(Allocator.TempJob);

            int entitiesCount = math.min(destoryEntities.Length, math.abs(diff));

            for(int i = 0; i < entitiesCount; i++)
            {
                inState.EntityManager.DestroyEntity(destoryEntities[i]);
                spawnData.CurrCount--;
            }

            destoryEntities.Dispose();
        }

        spawnData.Timer = 0f;
    }

    spawner.ValueRW = spawnData;
}
}
*/

/*
[BurstCompile]
public partial struct SpawnProcessJob : IJobEntity
{
    public SpawnerComponent Spawner;
    public EntityCommandBuffer.ParallelWriter ECB;

    [BurstCompile]
    public void Execute([EntityIndexInQuery] int inIndex)
    {            
        if (Spawner.CurrCount < Spawner.TargetCount)
        {
            var newEntity = ECB.Instantiate(inIndex, Spawner.Prefab);                
            ECB.SetComponent(inIndex, newEntity, new LocalTransform
            {
                Position = Spawner.SpawnPosition,
                Scale = 1.0f,
                Rotation = Spawner.SpawnRotation
            });
            ECB.AddComponent(inIndex, newEntity, new UnitComponent { });

            Spawner.CurrCount++;
        }
        else if (Spawner.CurrCount > Spawner.TargetCount)
        {
            Spawner.CurrCount--;
        }
    }
}
*/
