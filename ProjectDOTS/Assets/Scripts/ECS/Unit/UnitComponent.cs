using Unity.Entities;
using UnityEngine;

public struct UnitComponent : IComponentData
{
    // 인덱스
    public int Index;

    // 사용 여부
    public bool Activate;
}

