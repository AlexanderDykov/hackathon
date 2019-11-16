using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameScene.ECS.Components
{
    [Game, Unique, Event(EventTarget.Any)]
    public class LifeTimeComponent : IComponent
    {
        public int Value;
    }
}