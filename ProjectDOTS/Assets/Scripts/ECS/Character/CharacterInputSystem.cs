using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct CharacterInputSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState inState)
    {
        inState.RequireForUpdate<CharacterMovementComponent>();
        inState.RequireForUpdate<AttackComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState inState)
    {
        foreach(var (playerInput, isAttack) in SystemAPI.Query<RefRW<CharacterMovementComponent>, RefRW<AttackComponent>>())
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            playerInput.ValueRW.MoveDirection = new float2(moveX, moveY);

            if(Input.GetMouseButton(0) && isAttack.ValueRO.IsAttacking == false)
            {
                isAttack.ValueRW.IsAttacking = true;
            }
        }
    }
}
