using Unity.Entities;
using UnityEngine;

public struct UnitComponent : IComponentData
{
    // ��� ����
    public bool Activate;

    // ü��
    public float HealthValue;
}
