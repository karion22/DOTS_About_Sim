using Unity.Entities;
using Unity.Mathematics;

public struct SpawnerComponent : IComponentData
{
    // ������ ���� ������
    public Entity Prefab;

    // ���� �ݰ�
    public float SpawnRadius;

    // �⺻ ȸ�� ����
    public quaternion SpawnRotation;

    // ���� �ֱ�
    public float SpawnInterval;

    // ���� ���� ����
    public int CurrCount;

    // ��ǥ ���� ����
    public int TargetCount;

    // Ÿ�̸�
    public float Timer;

    // ���� ����(�Ÿ�)
    public float SpawnRange;
}
