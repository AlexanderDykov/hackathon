using UnityEngine;

namespace GameScene.ECS.Utils
{
    public class RandomPositionGenerator
    {
        public Vector2 RandomPosition()
        {
            return Random.insideUnitCircle * 5;
        }
    }
}