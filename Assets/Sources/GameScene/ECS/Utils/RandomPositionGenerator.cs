using UnityEngine;

namespace GameScene.ECS.Utils
{
    public class RandomPositionGenerator
    {
        private Grid _grid;
        public RandomPositionGenerator(Grid grid)
        {
            _grid = grid;
        }
        public Vector2 RandomPosition()
        {
            var pos = Random.insideUnitCircle * 5;
            Vector3Int f = new Vector3Int((int)pos.x ,(int)pos.y, 0);
            return _grid.CellToWorld(f);;
        }

    }
}