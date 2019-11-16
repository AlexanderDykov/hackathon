using Core.Contexts;
using GameScene.ECS.Utils;

namespace GameScene.Factories
{
    public class UIFactory
    {
        public GameEntity CreateLifeTimeView(IGameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddResource("LifeTimeView");
            
            entity.AddLife(Constants.StartLifeValue);
            entity.AddLifeTimer(Constants.MaxTimerValue);
            entity.AddParent("Canvas");
            return entity;
        }
    }
}