using System;
using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems
{
    public class CombineTilesSystem  : ReactiveSystem<GameEntity>
    {
        private IGameContext _context;
        private Dictionary<TileType, Action<Vector2>> _tilesCombinationActions = new Dictionary<TileType, Action<Vector2>>();
        private System.Random _randomGen = new System.Random();

        public CombineTilesSystem(IGameContext context) : base(context)
        {
            _context = context;
            _tilesCombinationActions.Add(TileType.Earth | TileType.Soul,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             if (_randomGen.Next(2) == 0) {
                                                 tile.AddTile(pos, TileType.Grass);
                                             } else {
                                                 tile.AddTile(pos, TileType.PoisonGround);
                                             }
                                         });
            _tilesCombinationActions.Add(TileType.Earth | TileType.Water,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Sand);
                                         });
            _tilesCombinationActions.Add(TileType.Earth | TileType.Air,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Mountain);
                                         });
            _tilesCombinationActions.Add(TileType.Earth | TileType.Fire,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Lava);
                                         });
            _tilesCombinationActions.Add(TileType.Water | TileType.Soul,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             if (_randomGen.Next(2) == 0) {
                                                 tile.AddTile(pos, TileType.HealingWater);
                                             } else {
                                                 tile.AddTile(pos, TileType.PoisonWater);
                                             }
                                         });
            _tilesCombinationActions.Add(TileType.Water | TileType.Air,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Ice);
                                         });
            _tilesCombinationActions.Add(TileType.Water | TileType.Fire,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Fog);
                                         });
            _tilesCombinationActions.Add(TileType.Air | TileType.Soul,
                                         (Vector2 pos) => {
                                             var creature = _context.CreateEntity();
                                             if (_randomGen.Next(2) == 0) {
                                                 creature.AddResource(ResourceNames.Human);
                                             } else {
                                                 creature.AddResource(ResourceNames.Skeleton);
                                             }
                                             creature.AddInitialPosition(pos);
                                         });
            _tilesCombinationActions.Add(TileType.Air | TileType.Fire,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Coal);
                                         });
            _tilesCombinationActions.Add(TileType.Fire | TileType.Soul,
                                         (Vector2 pos) => {
                                             var creature = _context.CreateEntity();
                                             if (_randomGen.Next(2) == 0) {
                                                 creature.AddResource(ResourceNames.Human);
                                             } else {
                                                 creature.AddResource(ResourceNames.Zombie);
                                             }
                                             creature.AddInitialPosition(pos);
                                         });
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Tile.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTile;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var newTile in entities)
            {
                var newTilePos = newTile.tile.Position;
                var newTileType = newTile.tile.TileType;
                var oldTiles = _context.GetEntitiesWithIndexTilePosition(newTilePos);
                foreach (var oldTile in oldTiles)
                {
                    if (newTile != oldTile) {
                        var oldTileType = oldTile.tile.TileType;
                        if (_tilesCombinationActions.ContainsKey(newTileType | oldTileType)) {
                            _tilesCombinationActions[newTileType | oldTileType](newTilePos);
                            newTile.isDestroy = true;
                            UnityEngine.Object.Destroy(newTile.view.Value);
                            oldTile.isDestroy = true;
                            UnityEngine.Object.Destroy(oldTile.view.Value);
                            break;
                        }
                    }
                }
            }
        }
    }
}
