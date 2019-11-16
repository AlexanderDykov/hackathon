using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;

namespace Core.Contexts
{
    public interface IGameContext : IContext<GameEntity>
    {
        GameEntity playerEntity { get; }
        bool isShowSelectView { get; set; }
        void ReplaceLifeTimer(float newValue);
        GameEntity lifeTimerEntity { get; }
        LifeTimerComponent lifeTimer { get; }
        LifeComponent life { get; }
        void ReplaceLife(int newValue);
        bool isEndGame{ get; set; }
    }
}

partial class GameContext : IGameContext {}