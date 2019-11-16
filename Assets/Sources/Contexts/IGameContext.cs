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
        void ReplaceLifeTimer(float newValue);
        GameEntity lifeTimerEntity { get; }
        LifeTimerComponent lifeTimer { get; }
        LifeComponent life { get; }
        void ReplaceLife(int newValue);
        bool isEndGame{ get; set; }
        void ReplaceScore(int newValue);
        ScoreComponent score { get; }
        HashSet<GameEntity> GetEntitiesWithIndexTilePosition(Vector2 position);
    }
}

partial class GameContext : IGameContext
{
    public HashSet<GameEntity> GetEntitiesWithIndexTilePosition(Vector2 position)
    {
        return this.GetEntitiesWithTilePosition(position);
    }
}