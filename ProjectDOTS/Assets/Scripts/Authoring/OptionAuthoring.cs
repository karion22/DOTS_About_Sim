using Unity.Entities;
using UnityEngine;

public class OptionAuthoring : MonoBehaviour
{
    public eOptionMode OptionMode;

    public class Baker : Baker<OptionAuthoring>
    {
        public override void Bake(OptionAuthoring inAuthoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new OptionComponent { Mode = inAuthoring.OptionMode });
        }
    }
}