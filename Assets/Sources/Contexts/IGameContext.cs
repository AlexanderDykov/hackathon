using System.Collections.Generic;
using Core.Contexts;
using Entitas;
using GameScene.ECS.Components;
using UnityEngine;

namespace Core.Contexts
{
    public interface IGameContext : IContext<GameEntity>
    {
        GameEntity playerEntity { get; }
        bool isShowSelectView { get; set; }
        BalanceComponent balance { get; }
        void ReplaceBalance(int newValue);
        bool isEndGame{ get; set; }
        void ReplaceScore(int newValue);
        ScoreComponent score { get; }
        void ReplaceWorld(Vector2Int size);
        GameEntity worldEntity { get; }
        HashSet<GameEntity> GetEntitiesWithIndexTilePosition(Vector2 position);
//        HashSet<GameEntity> EntitiesWithInitialPosition(Vector2 position);
        HashSet<GameEntity> EntitiesWithCellPosition(Vector3Int position);
    }
}

partial class GameContext : IGameContext
{
    public HashSet<GameEntity> GetEntitiesWithIndexTilePosition(Vector2 position)
    {
        return this.GetEntitiesWithTilePosition(position);
    }

//    public HashSet<GameEntity> EntitiesWithInitialPosition(Vector2 position)
//    {
//        return this.GetEntitiesWithInitialPosition(position);
//    }
    
    public HashSet<GameEntity> EntitiesWithCellPosition(Vector3Int position)
    {
        return this.GetEntitiesWithCell(position);
    }
}
