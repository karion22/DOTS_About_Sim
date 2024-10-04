using Unity.Entities;
using UnityEngine;

public struct AttackComponent : IComponentData
{
    // 공격 상황인지 확안
    public bool IsAttacking;

    // 공격 범위
    public float Range;

    // 공격 데미지
    public float Damage;
}