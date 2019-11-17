using System;
using System.Collections.Generic;
using Core.Contexts;
using GameScene.ECS.Components;
using GameScene.Utils;
using UnityEngine;

namespace GameScene.Factories
{
    public class MonsterFactory
    {
//        *** TODO white
//        1. statue -> human(hp + walking)
//            2. human -> warrior(+attack) or worker(+build)
//        3. warrior -> healer
//        4. worker -> prist
//            *** TODO black
//        1. statue -> skeleton(hp + walking + attack)
//        2. skeleton -> zombie(-attack +disease spell) or worker(-attack +build)
//        4. worker -> lich
//            ** TODO unit balances
//        1. White



//        4. Healer: 15 HP, +2 Heal on 3 radius
//        5. Prist: 20 HP, [create soul near house](10 seconds){30 seconds cooldown}
//        2. Black

//        2. Zombie: 10 HP, [human,worker -> skeleton](need to stay around 3 seconds)
//        3. Worker: 15 HP, [build house](10 seconds){30 seconds cooldown}
//        4. Lich: 20 HP, [create skeleton near house](10 seconds){30 seconds cooldown}

        private IGameContext _context;
        private Dictionary<CreatureType, Action<Vector2>> map;
        
        public MonsterFactory(IGameContext context)
        {
            _context = context;
            map = new Dictionary<CreatureType, Action<Vector2>>()
            {
                {CreatureType.Statue, CreateHuman}
            };
        }

        // 1. Human: 10 HP
        public void CreateHuman( Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Human);
            entity.AddCreatureType(CreatureType.Human);
            entity.AddHealth(10);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1);
        }
        
        // 2. Warrior: 10 HP, 4 Attack
        public void CreateWarrior(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Warrior);
            entity.AddCreatureType(CreatureType.Warrior);
            entity.AddHealth(10);
            entity.isPhysic = true;
            entity.isAttackable = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1);
            
            entity.AddAttackPower(4);
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
            entity.AddAttackDistance(1f);
        }
        
        // 3. Worker: 15 HP, [build house](10 seconds){30 seconds cooldown}
        public void CreateWorker(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Warrior);
            entity.AddCreatureType(CreatureType.Warrior);
            entity.AddHealth(15);
            entity.isPhysic = true;
            entity.isAttackable = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1.5f);

            entity.AddCreator(CreatureType.Statue);
            
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
        }
        
        // 1. Skeleton: 8 HP, 4 Attack
        public void CreateSkeleton(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Skeleton);
            entity.AddCreatureType(CreatureType.Skeleton);
            entity.AddHealth(8);
            entity.isPhysic = true;
            entity.isAttackable = true;
            entity.isDamagable = true;
            entity.AddSide(Side.Black);
            entity.AddSpeed(1);
            
            entity.AddAttackPower(2);
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
            entity.AddAttackDistance(1f);
        }

        public void Create(CreatureType creatureType, Vector2 position)
        {
            if (map.ContainsKey(creatureType))
            {
                map[creatureType].Invoke(position);
            }
        }
    }
}

[Flags]
public enum CreatureType
{
    Soul = 1,
    Statue = 1 << 1 ,
    Human = 1 << 2,
    Warrior = 1 << 3,
    Worker= 1 << 4,
    Healer= 1 << 5,
    Priest = 1 << 6,
    Skeleton = 1 << 7,
    Zombie = 1 << 8,
    EvilWorker = 1 << 9,
    Lich = 1 << 10,
    Random = 1 << 11,
}