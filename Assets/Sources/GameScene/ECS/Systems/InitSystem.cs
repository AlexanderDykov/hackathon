using Core.Contexts;
using Entitas;
using SpaceWars.GameScene.Factories;

namespace GameScene.ECS.Systems
{
    public sealed class InitSystem : IInitializeSystem
    {
        private readonly IGameContext _context;
        private readonly IPlayerFactory _playerFactory;
        
        public InitSystem(IGameContext context, IPlayerFactory playerFactory)
        {
            _context = context;
            _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _playerFactory.CreatePlayer(_context);
        }
    }
}