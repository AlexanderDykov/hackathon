using Core.Contexts;
using Entitas;

namespace Core.Contexts
{
    public interface IInputContext : IContext<InputEntity>
    {
        InputEntity inputEntity { get; }
    }
}

partial class InputContext : IInputContext {}