using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CharacterAuthroing : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5.0f;
    public class CharacterBaker : Baker<CharacterAuthroing>
    {
        public override void Bake(CharacterAuthroing authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CharacterComponent { MoveSpeed = authoring.MoveSpeed });
            AddComponent(entity, new CharacterInput { MoveDirection = float2.zero });
        }
    }
}
