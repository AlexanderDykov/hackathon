using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.View;

namespace GameScene.ECS.Systems
{
    public class ShowPanelSystem: ReactiveSystem<GameEntity>
    {
        private SelectPanel _selectPanel;
        
        public ShowPanelSystem(IGameContext gameContext, SelectPanel panel) : base(gameContext)
        {
            _selectPanel = panel;
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ShowSelectView.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasBoxSkills;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.isShowSelectView)
                {
                    _selectPanel.Show(entity);
                    return;
                }
                _selectPanel.Hide();
                return;
            }
        }
    }
}