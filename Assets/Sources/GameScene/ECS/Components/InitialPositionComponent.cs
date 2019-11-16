using Entitas;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Game]
    public class InitialPositionComponent : IComponent
    {
        public Vector2 Value;
    }
}