using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(CharacterAttackSystem))]
public partial struct EnemyAttackSystem : ISystem
{

}
