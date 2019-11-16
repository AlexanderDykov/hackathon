using System;
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

    [Flags]
    public enum TileType
    {
        None = -1,
        Earth = 1,
        Water = 1 << 1,
        Air = 1 << 2,
        Fire = 1 << 3,
        Soul = 1 << 4,
        Grass = 1 << 5,
        PoisonGround = 1 << 6,
        Sand = 1 << 7,
        Mountain = 1 << 8,
        Lava = 1 << 9,
        HealingWater = 1 << 10,
        PoisonWater = 1 << 11,
        Ice = 1 << 12,
        Fog = 1 << 13,
        Coal = 1 << 14
    }
}
