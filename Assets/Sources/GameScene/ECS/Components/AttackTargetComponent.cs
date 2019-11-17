using Entitas;

namespace GameScene.ECS.Components
{
    [Game]
    public class AttackTargetComponent : IComponent
    {
        public GameEntity Value;
    }
}