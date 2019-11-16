using Core.Contexts;

namespace GameScene.View
{
    public interface IView
    {
        void Link(IGameContext context, GameEntity entity);
    }
}