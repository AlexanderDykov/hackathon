using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class AddParentSystem : ReactiveSystem<GameEntity>
    {
        public AddParentSystem(IGameContext context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Parent.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasView && entity.hasParent;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                //TODO: fix it
                var parent = GameObject.Find(entity.parent.Name).transform;
                entity.view.Value.transform.SetParent(parent, false);
            }
        }
    }
}