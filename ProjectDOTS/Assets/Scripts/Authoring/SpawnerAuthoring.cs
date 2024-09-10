using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnInterval;
    public int CurrCount;
    public int TargetCount;

    class SpanwerAuthoringBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Renderable);

            var spawnPosition = authoring.transform.position;
            spawnPosition.x -= 1.0f;

            AddComponent(entity, new SpawnerComponent
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = spawnPosition,
                SpawnRotation = authoring.transform.rotation,
                SpawnInterval = authoring.SpawnInterval,
                CurrCount = authoring.CurrCount,
                TargetCount = authoring.TargetCount
            });
        }
    }
}
