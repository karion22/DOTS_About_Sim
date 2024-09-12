using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct CharacterInputSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        foreach(var (playerInput, entity) in SystemAPI.Query<RefRW<CharacterInput>>().WithEntityAccess())
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            playerInput.ValueRW.MoveDirection = new float2(moveX, moveY);
        }
    }
}
