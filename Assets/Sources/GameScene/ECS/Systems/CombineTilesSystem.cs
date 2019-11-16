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
            _tilesCombinationActions.Add(TileType.Water | TileType.Air,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Ice);
                                         });
            _tilesCombinationActions.Add(TileType.Water | TileType.Fire,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Sand);
                                         });
            _tilesCombinationActions.Add(TileType.Air | TileType.Fire,
                                         (Vector2 pos) => {
                                             var tile = _context.CreateEntity();
                                             tile.AddTile(pos, TileType.Coal);
                                         });
            _tilesCombinationActions.Add(TileType.Air | TileType.None,
                                         (Vector2 pos) => {});
            _tilesCombinationActions.Add(TileType.Fire | TileType.None,
                                         (Vector2 pos) => {});
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
                GameEntity oldTileFoo = null;
                var oldTileType = TileType.None;
                foreach (var oldTile in oldTiles)
                {
                    if (oldTile != newTile)
                    {
                        oldTileFoo = oldTile;
                        oldTileType = oldTile.tile.TileType;
                        break;
                    }
                }
                if (newTileType != oldTileType)
                {
                    if (_tilesCombinationActions.ContainsKey(newTileType | oldTileType))
                    {
                        _tilesCombinationActions[newTileType | oldTileType](newTilePos);
                        newTile.isDestroy = true;
                        UnityEngine.Object.Destroy(newTile.view.Value);
                        if (oldTileFoo != null)
                        {
                            oldTileFoo.isDestroy = true;
                            UnityEngine.Object.Destroy(oldTileFoo.view.Value);
                        }
                    }
                } else {
                    if (oldTileFoo != null)
                    {
                        oldTileFoo.isDestroy = true;
                        UnityEngine.Object.Destroy(oldTileFoo.view.Value);
                    }
                }
            }
        }
    }
}
