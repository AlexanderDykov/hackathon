using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.Factories;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class InitSystem : IInitializeSystem
    {
        private readonly IGameContext _context;
        private readonly IPlayerFactory _playerFactory;
        private readonly IBoxFactory _boxFactory;
        private readonly UIFactory _uiFactory;

        public InitSystem(IGameContext context,
            IPlayerFactory playerFactory,
            IBoxFactory boxFactory,
            UIFactory uiFactory)
        {
            _context = context;
            _playerFactory = playerFactory;
            _boxFactory = boxFactory;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _playerFactory.CreatePlayer(_context);

            _uiFactory.CreatePlayerHUD(_context);

            _uiFactory.CreateCamera(_context);

            _boxFactory.CreateEntity(_context, Vector2.zero, TileType.None);
        }
    }
}
