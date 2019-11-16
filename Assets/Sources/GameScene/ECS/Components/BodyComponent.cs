using Entitas;
using UnityEngine;

namespace GameScene.ECS.Components
{
    public class BodyComponent : IComponent
    {
        public Rigidbody2D Value;
    }
}