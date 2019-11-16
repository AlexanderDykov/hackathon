using Core.Contexts;
using Entitas;

namespace Core.Contexts
{
    public interface IGameContext : IContext<GameEntity>
    {
        GameEntity playerEntity { get; }
        bool isShowSelectView { get; set; }
    }
}

partial class GameContext : IGameContext {}