using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace GameScene.ECS.Components
{
    [Game]
    public class TileComponent : IComponent
    {
        [EntityIndex]
        public Vector2 Position;

        [EntityIndex]
        public TileType TileType;
    }

    public enum TileType
    {
        Earth, Water, Air, Fire,Soul
    }
}