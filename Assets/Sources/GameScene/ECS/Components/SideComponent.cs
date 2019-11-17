using Entitas;

namespace GameScene.ECS.Components
{
    [Game]
    public class SideComponent : IComponent
    {
        public Side Value;
    }

    public enum Side
    {
        White,
        Black
    }
}