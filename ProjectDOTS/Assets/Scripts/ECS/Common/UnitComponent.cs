using Unity.Entities;
using Unity.Mathematics;

public struct UnitComponent : IComponentData
{
    // ��� ����
    public bool Activate;

    // ü��
    public float HealthValue;
}