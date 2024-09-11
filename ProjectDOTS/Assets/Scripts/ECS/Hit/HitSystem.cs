using Unity.Burst;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
public partial struct HitSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState inState)
    {
        inState.RequireForUpdate<HitComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {

    }
}
