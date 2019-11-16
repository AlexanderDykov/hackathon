using Core.Contexts;
using GameScene.ECS.Utils;
using GameScene.Utils;

namespace GameScene.Factories
{
    public class UIFactory
    {
        public GameEntity CreatePlayerHUD(IGameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddResource(ResourceNames.PlayerHUD);
            entity.AddLife(Constants.StartLifeValue);
            entity.AddLifeTimer(Constants.MaxTimerValue);
            entity.AddScore(0);
            entity.AddParent(ResourceNames.Canvas);
            return entity;
        }

        public void CreateCamera(IGameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddResource(ResourceNames.Camera);
            entity.AddParent(ResourceNames.Player);
        }
    }
}