using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class PlayerInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _player;
        private const float StartMovingCoefficient = 0.05f;

        public PlayerInputSystem(IGameContext gameContext)
        {
            _player = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Player));
        }

        public void Execute()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            foreach (var player in _player)
            {
                player.ReplaceDirection(new Vector3(horizontal, vertical, 0f));
            }
        }

    }
}
