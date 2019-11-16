using System.Collections.Generic;
using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.ECS.Systems.Skill
{
    public class CreateTileBySkillSystem: CreateCreatureBySkillSystem
    {
        private Grid _grid;
        public CreateTileBySkillSystem(IGameContext context, Grid grid) : base(context)
        {
            _grid = grid;
        }
        
        private Dictionary<SkillType, TileType> map = new Dictionary<SkillType, TileType>()
        {
            {SkillType.CreateFire, TileType.Fire},
            {SkillType.CreateWater, TileType.Water},
            {SkillType.CreateAir, TileType.Air},
            {SkillType.CreateEarth, TileType.Earth},
            {SkillType.CreateSoul, TileType.Soul},
        };

        protected override void CreateCreature(GameEntity entity, Vector3 transformPosition)
        {
            CreateTile(entity, transformPosition);
            if(entity.skill.Type == SkillType.CreateSoul)
                return;
            
            var gridPos = _grid.WorldToCell(transformPosition);

            var leftPos = _grid.CellToWorld(new Vector3Int(gridPos.x - 1, gridPos.y, gridPos.z));
            var rightPos = _grid.CellToWorld(new Vector3Int(gridPos.x + 1, gridPos.y, gridPos.z));
            var topPos = _grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y + 1, gridPos.z));
            var bottomPos = _grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y - 1, gridPos.z));

            CreateTile(entity, leftPos);
            CreateTile(entity, rightPos);
            CreateTile(entity, topPos);
            CreateTile(entity, bottomPos);
        }

        private void CreateTile(GameEntity entity, Vector3 transformPosition)
        {
            var soulEntity = Context.CreateEntity();
            soulEntity.AddResource(map[entity.skill.Type].ToString());
            soulEntity.AddInitialPosition(transformPosition);
        }

        protected override bool CheckSkillType(GameEntity entity)
        {
            return (entity.skill.Type & SkillType.Tile) != 0;
        }
    }
}