using Core.Contexts;
using GameScene.ECS.Utils;

namespace GameScene.Factories
{
    public class UIFactory
    {
        public GameEntity CreatePlayerHUD(IGameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddResource("PlayerHUD");
            
            entity.AddLife(Constants.StartLifeValue);
            entity.AddLifeTimer(Constants.MaxTimerValue);
            entity.AddScore(0);
            entity.AddParent("Canvas");
            return entity;
        }
    }
}