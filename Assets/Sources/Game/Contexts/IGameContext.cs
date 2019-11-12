using Core.Contexts;
using Entitas;

namespace Core.Contexts
{
    public interface IGameContext : IContext<GameEntity>
    {
    }
}

partial class GameContext : IGameContext {}