using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Game]
    public class CellComponent : IComponent
    {
        [EntityIndex]
        public Vector3Int Position;
    }
}
