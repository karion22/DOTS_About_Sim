using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(CharacterInputSystem))]
[BurstCompile]
public partial struct CharacterSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach(var (localTransform, characterInput, characterInfo) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CharacterInput>, RefRO<CharacterComponent>>())
        {
            float3 move = new float3(characterInput.ValueRO.MoveDirection.x, 0, characterInput.ValueRO.MoveDirection.y);
            localTransform.ValueRW.Position += move * dt * characterInfo.ValueRO.MoveSpeed;
        }
    }
}
