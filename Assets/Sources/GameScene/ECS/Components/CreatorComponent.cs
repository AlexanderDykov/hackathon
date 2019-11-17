using Entitas;

namespace GameScene.ECS.Components
{
    [Game]
    public class CreatorComponent : IComponent
    {
        public CreatureType Value;
    }
}