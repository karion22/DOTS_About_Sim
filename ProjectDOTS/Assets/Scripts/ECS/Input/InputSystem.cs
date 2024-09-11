using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    [BurstCompile]
    public void OnCreate(ref SystemState inState)
    {
        inState.RequireForUpdate<InputComponent>();
    }

    protected override void OnUpdate()
    {
        var hit = SystemAPI.GetSingletonRW<InputComponent>();
        hit.ValueRW.IsHitChanged = false;

        if (Camera.main == null || !Input.GetMouseButton(0)) return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(new Plane(Vector3.up, 0f).Raycast(ray, out var dist))
        {
            hit.ValueRW.IsHitChanged = true;
            hit.ValueRW.HitPoint = ray.GetPoint(dist);
        }
    }
}