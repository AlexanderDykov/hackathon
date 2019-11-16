using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public sealed class AddViewSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGameContext _context;
        public AddViewSystem(IGameContext context) : base(context)
        {
            _context = context;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Resource.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasResource;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var prefab = Resources.Load<GameObject>(entity.resource.Path);
                var go =  Object.Instantiate(prefab);
                entity.AddView(go);
                go.Link(entity, _context);
                entity.RemoveResource();
            }
        }
    }

}