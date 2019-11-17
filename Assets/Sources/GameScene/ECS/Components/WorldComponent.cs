using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Game, Unique]
    public class WorldComponent : IComponent
    {
        public Vector2Int Size;
    }
}
