using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class InitWorldSystem : IInitializeSystem
    {
        private readonly IGameContext _context;

        public InitWorldSystem(IGameContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            var worldSize = _context.worldEntity.world.Size;
            var halfWidth = worldSize.x / 2;
            var halfHeight = worldSize.y / 2;
            for (int i = -halfWidth; i < halfWidth; ++i)
            {
                var reputation = (i < 0) ? -1 : 1;
                for (int j = -halfHeight; j < halfHeight; ++j)
                {
                    var cellEntity = _context.CreateEntity();
                    cellEntity.AddCell(new Vector3Int(i, j, 0));
                    cellEntity.AddReputation(reputation);
                }
            }

        }
    }
}
