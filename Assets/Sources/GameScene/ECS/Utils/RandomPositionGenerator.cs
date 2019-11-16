using UnityEngine;

namespace GameScene.ECS.Utils
{
    public class RandomPositionGenerator
    {
        public Vector2 RandomPosition()
        {
            int rndX = Random.Range(0, 20);
            int rndY = Random.Range(0, 20);
            return new Vector2(rndX, rndY);
        }
    }
}