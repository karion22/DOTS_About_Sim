using Unity.Entities;
using UnityEngine;

public struct AttackComponent : IComponentData
{
    // ���� ��Ȳ���� Ȯ��
    public bool IsAttacking;

    // ���� ����
    public float Range;

    // ���� ������
    public float Damage;
}