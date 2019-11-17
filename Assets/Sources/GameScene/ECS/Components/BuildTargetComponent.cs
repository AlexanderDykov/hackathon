using Entitas;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Game]
    public class BuildTargetComponent : IComponent
    {
        public Vector2 Position;
    }
}