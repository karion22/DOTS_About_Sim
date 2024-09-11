using Unity.Entities;
using UnityEngine;

public class CharacterAuthroing : MonoBehaviour
{

    public class CharacterBaker : Baker<CharacterAuthroing>
    {
        public override void Bake(CharacterAuthroing authoring)
        {

        }
    }
}
