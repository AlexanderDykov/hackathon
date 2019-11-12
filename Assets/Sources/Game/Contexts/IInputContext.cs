using Core.Contexts;
using Entitas;

namespace Core.Contexts
{
    public interface IInputContext : IContext<InputEntity>
    {
    }
}

partial class InputContext : IInputContext {}