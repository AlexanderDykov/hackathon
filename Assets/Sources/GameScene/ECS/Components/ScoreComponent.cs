using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameScene.ECS.Components
{
    [Game, Unique, Event(EventTarget.Self)]
    public class ScoreComponent : IComponent
    {
        public int Value;
    }
}