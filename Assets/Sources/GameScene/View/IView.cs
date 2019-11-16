using Core.Contexts;

namespace GameScene.View
{
    public interface IView
    {
        void Link(IGameContext context, GameEntity entity);
    }
    
    public interface IDamagable
    {
        void Damage(int value);
    }
}