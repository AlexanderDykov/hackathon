using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Input, Unique]
    public class InputComponent : IComponent
    {
        public Vector2 Value;
    }
}