using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace GameScene.ECS.Components
{
    [Game, Unique, Event(EventTarget.Self)]
    public class BalanceComponent : IComponent
    {
        public int Value;
    }
}
