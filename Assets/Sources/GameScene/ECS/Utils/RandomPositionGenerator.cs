using System.Collections.Generic;
using System.Linq;
using Core.Contexts;
using UnityEngine;

namespace GameScene.ECS.Utils
{
    public class RandomPositionGenerator
    {
        private Grid _grid;
        private IGameContext _context;
        public RandomPositionGenerator(Grid grid, IGameContext context)
        {
            _grid = grid;
            _context = context;
        }
        
        

        public Vector3Int RandomPosition()
        {
            List<GameEntity> posito = new List<GameEntity>();
            var group = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Cell)).GetEntities();
            posito = group.Where(x => x.hasTile).ToList();
            var pos2 = group.Where(x => !x.hasTile && !x.hasSpeed);
            
            foreach (var gameEntity in pos2)
            {
                posito.Remove(gameEntity);
            } 
            
            if (posito.Count > 0)
            {
                var range = Random.Range(0, posito.Count - 1);
                return posito[range].cell.Position;
            }
            
            return  _grid.WorldToCell(Random.insideUnitCircle * 50);
        }
    }
}