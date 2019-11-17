using System;
using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.Factories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameScene.ECS.Systems
{
    public class UpgradeSystem : ReactiveSystem<GameEntity>
    {
        
//        1. statue -> human(hp + walking)
//            2. human -> warrior(+attack) or worker(+build)
//        3. warrior -> healer
//        4. worker -> prist
//            *** TODO black
//        1. statue -> skeleton(hp + walking + attack)
//        2. skeleton -> zombie(-attack +disease spell) or worker(-attack +build)
//        4. worker -> lich
        private IGameContext _context;
        
        Dictionary<CreatureType, Action<Vector3Int>> upgrade;

        private Grid _grid;
        public UpgradeSystem(IGameContext context, MonsterFactory monsterFactory, Grid grid) : base(context)
        {
            _context = context;
            _grid = grid;
            upgrade = new Dictionary<CreatureType, Action<Vector3Int>>()
            {
                // white
                {CreatureType.Statue, monsterFactory.CreateHuman},
                {CreatureType.Human, (pos) =>
                    {
                        if (UnityEngine.Random.Range(0, 2) < 0.5)
                        {
                            monsterFactory.CreateWarrior(pos);
                        }
                        else
                        {
                            monsterFactory.CreateWorker(pos);
                        }
                    }
                },
                {CreatureType.Worker, monsterFactory.CreatePrist},
                {CreatureType.Warrior, monsterFactory.CreateHealer},
                // black
                {CreatureType.BlackStatue, monsterFactory.CreateSkeleton},
                {CreatureType.Skeleton, (pos) =>
                {
                    if (UnityEngine.Random.Range(0, 2) < 0.5)
                    {
                        monsterFactory.CreateBlackWorker(pos);
                    }
                    else
                    {
                        monsterFactory.CreateZombie(pos);
                    }
                }},
                {CreatureType.BlackWorker, monsterFactory.CreateLich},
            };
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Upgrade.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasUpgrade && entity.upgrade.Entity.hasCreatureType;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var upgradeEntity = entity.upgrade.Entity;
                var creatureTypeValue = upgradeEntity.creatureType.Value;
                
                if (!upgrade.ContainsKey(creatureTypeValue)) continue;
                upgrade[creatureTypeValue].Invoke(_grid.WorldToCell(upgradeEntity.view.Value.transform.position));
                Object.Destroy(upgradeEntity.view.Value);
                upgradeEntity.isDestroy = true;
            }
        }
    }
}
