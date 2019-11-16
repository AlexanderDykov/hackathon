using Core.Contexts;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.Factories
{
    public interface IBoxFactory
    {
        GameEntity CreateEntity(IGameContext context, Vector2 position);
    }

    public class BoxFactory : IBoxFactory
    {
        public GameEntity CreateEntity(IGameContext context, Vector2 position)
        {
            var playerEntity = context.CreateEntity();
            playerEntity.AddInitialPosition(position);
            playerEntity.AddResource(ResourceNames.Box);
            return playerEntity;
        }
    }
}
