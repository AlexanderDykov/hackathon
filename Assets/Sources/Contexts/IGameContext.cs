using Core.Contexts;
using Entitas;

namespace Core.Contexts
{
    public interface IGameContext : IContext<GameEntity>
    {
        GameEntity playerEntity { get; }
    }
}

partial class GameContext : IGameContext {}