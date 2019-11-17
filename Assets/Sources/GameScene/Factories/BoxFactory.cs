using System.Collections.Generic;
using System.Linq;
using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.Factories
{
    public interface IBoxFactory
    {
        GameEntity CreateEntity(Vector3Int position);
    }

    public class BoxFactory : IBoxFactory
    {
        private static Skill _statueSkill = new Skill("Create statue", SkillType.CreateStatue );
        private static Skill _blackStatueSkill = new Skill("Create statue", SkillType.CreateBlackStatue );
        private static readonly Skill CreateSoulSkill = new Skill("Create soul", SkillType.CreateSoul );
        private IGameContext _context;
        private Grid _grid;
        public BoxFactory(IGameContext context, Grid grid)
        {
            _context = context;
            _grid = grid;
        }

        public GameEntity CreateEntity( Vector3Int position)
        {
            var playerEntity = _context.CreateEntity();

            var cellPos = _grid.WorldToCell(position);

            var isWhite = true;
            
            var cells = _context.EntitiesWithCellPosition(cellPos);
            if (cells.Count > 0)
            {
                isWhite = cells.FirstOrDefault(x => x.hasReputation)?.reputation?.Value > 0;
            }

            playerEntity.AddCell(position);
            var newSkills = new List<Skill> {CreateSoulSkill, isWhite ? _statueSkill : _blackStatueSkill};

            playerEntity.AddBoxSkills(newSkills);
            playerEntity.AddResource(ResourceNames.Box);
            return playerEntity;
        }
    }
}
