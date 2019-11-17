using System.Collections.Generic;
using Core.Contexts;
using Entitas;

namespace GameScene.ECS.Systems
{
    public class EndGameSystem: ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        private IInputContext _contextS;

        private EndGame _panel;
        public EndGameSystem(IGameContext context, IInputContext contextS, EndGame panel)  : base(context)
        {
            _context = context;
            _contextS = contextS;
            _panel = panel;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.EndGame.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isEndGame;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _panel.gameObject.SetActive(true);
        }
    }
}