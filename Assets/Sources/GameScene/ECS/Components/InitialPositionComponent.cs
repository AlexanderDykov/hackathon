using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Game]
    public class InitialPositionComponent : IComponent
    {
        [EntityIndex]
        public Vector2 Value;
    }
}