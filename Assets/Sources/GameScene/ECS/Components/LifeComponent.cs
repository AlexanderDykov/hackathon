using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameScene.ECS.Components
{
    [Game, Unique, Event(EventTarget.Self)]
    public class LifeComponent : IComponent
    {
        public int Value;
    }
}