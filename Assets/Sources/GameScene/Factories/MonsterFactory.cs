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
                {CreatureType.Statue, CreateStatue},
                {CreatureType.Warrior, CreateWarrior},
                {CreatureType.WhiteBuilding, CreateWhiteBuilding},
                {CreatureType.BlackBuilding, CreateBlackBuilding},
                {CreatureType.Soul, CreateSoul},
                {CreatureType.Skeleton, CreateSkeleton},
            };
        }

        public void CreateWhiteBuilding(Vector2 transformPosition)
        {
            var statue = _context.CreateEntity();
            statue.AddResource(ResourceNames.WhiteBuilding);
            statue.AddInitialPosition(transformPosition);
            statue.AddCreatureType(CreatureType.WhiteBuilding);
        }


        // 1. Human: 10 HP
        public void CreateHuman( Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Human);
            entity.AddCreatureType(CreatureType.Human);
            entity.AddInitialHealth(10);
            entity.AddHealth(10);
            entity.AddDistanceToTarget(1f);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1);
            entity.AddReputation(1);
        }
        
        // 2. Warrior: 10 HP, 4 Attack
        public void CreateWarrior(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Warrior);
            entity.AddCreatureType(CreatureType.Warrior);
            entity.AddInitialHealth(10);
            entity.AddHealth(10);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1);
            entity.AddDistanceToTarget(1f);
            
            entity.AddAttackPower(4);
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
        }
        
        // 3. Worker: 15 HP, [build house](10 seconds){30 seconds cooldown}
        public void CreateWorker(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Worker);
            entity.AddCreatureType(CreatureType.Worker);
            entity.AddInitialHealth(15);
            entity.AddHealth(15);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1.5f);

            entity.AddCreator(CreatureType.WhiteBuilding);
            
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
            entity.AddDistanceToTarget(1f);
            
            entity.AddLookNearest(CreatureType.Position);
        }

        // 4. Healer: 15 HP, +2 Heal on 3 radius{5 seconds cooldown}
        public void CreateHealer(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Healer);
            entity.AddCreatureType(CreatureType.Healer);
            entity.AddInitialHealth(15);
            entity.AddHealth(15);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1.5f);

            entity.AddHealPower(2);

            entity.AddInitialCalldown(2);
            entity.AddCalldown(2);
            entity.AddDistanceToTarget(3f);

            entity.AddLookNearest(CreatureType.Human | CreatureType.Warrior | CreatureType.Worker |  CreatureType.Priest);
        }

        // 1. Skeleton: 8 HP, 4 Attack
        public void CreateSkeleton(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Skeleton);
            entity.AddCreatureType(CreatureType.Skeleton);
            entity.AddInitialHealth(8);
            entity.AddHealth(8);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.Black);
            entity.AddSpeed(1);
            entity.AddDistanceToTarget(1f);
            
            entity.AddAttackPower(2);
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
            
            entity.AddLookNearest(CreatureType.Human | CreatureType.Warrior | CreatureType.Worker);
        }

        public void Create(CreatureType creatureType, Vector2 position)
        {
            if (map.ContainsKey(creatureType))
            {
                map[creatureType].Invoke(position);
            }
        }

        public void CreateStatue(Vector2 transformPosition)
        {
            var soul = _context.CreateEntity();
            soul.AddResource(ResourceNames.Statue);
            soul.AddInitialPosition(transformPosition);
            soul.AddCreatureType(CreatureType.Statue);
        }
        
        public void CreateSoul(Vector2 position)
        {
            var soul = _context.CreateEntity();
            soul.AddResource(ResourceNames.Soul);
            soul.AddInitialPosition(position);
            soul.AddCreatureType(CreatureType.Soul);
            soul.isPhysic = true;
            soul.AddSpeed(3);
            soul.isSoul = true;
            soul.AddDistanceToTarget(1f);
            soul.AddLookNearest(
                CreatureType.Statue 
                |CreatureType.Human
                |CreatureType.Warrior 
                |CreatureType.Worker
                |CreatureType.BlackStatue
                |CreatureType.Skeleton
                |CreatureType.BlackWorker);
        }

        // 5. Prist: 20 HP, [create soul near house](10 seconds){30 seconds cooldown}
        public void CreatePrist(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Priest);
            entity.AddCreatureType(CreatureType.Priest);
            entity.AddInitialHealth(20);
            entity.AddHealth(20);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1.5f);

            entity.AddCreator(CreatureType.Soul);
            
            entity.AddCalldown(4);
            entity.AddInitialCalldown(4);
            entity.AddDistanceToTarget(1f);
            
            entity.AddLookNearest(CreatureType.WhiteBuilding);
        }

        public void CreateBlackWorker(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Worker);
            entity.AddCreatureType(CreatureType.Worker);
            entity.AddInitialHealth(15);
            entity.AddHealth(15);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.Black);
            entity.AddSpeed(1.5f);

            entity.AddCreator(CreatureType.BlackBuilding);
            
            entity.AddCalldown(2);
            entity.AddInitialCalldown(2);
            entity.AddDistanceToTarget(1f);
            
            entity.AddLookNearest(CreatureType.Position);
        }

        public void CreateZombie(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Zombie);
            entity.AddCreatureType(CreatureType.Zombie);
            entity.AddInitialHealth(10);
            entity.AddHealth(10);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.Black);
            entity.AddSpeed(1);
            entity.AddDistanceToTarget(1f);
            
            entity.AddZombieTimer(3);
            entity.AddCalldown(3);
            entity.AddInitialCalldown(3);
            
            entity.AddLookNearest(CreatureType.Human | CreatureType.Worker);
        }

        public void CreateLich(Vector2 position)
        {
            var entity = _context.CreateEntity();
            entity.AddInitialPosition(position);
            entity.AddResource(ResourceNames.Lich);
            entity.AddCreatureType(CreatureType.Lich);
            entity.AddInitialHealth(20);
            entity.AddHealth(20);
            entity.isPhysic = true;
            entity.isDamagable = true;
            entity.AddSide(Side.White);
            entity.AddSpeed(1.5f);

            entity.AddCreator(CreatureType.Skeleton);
            
            entity.AddCalldown(4);
            entity.AddInitialCalldown(4);
            entity.AddDistanceToTarget(1f);
            
            entity.AddLookNearest(CreatureType.BlackBuilding);
        }

        public void CreateBlackStatue(Vector3 transformPosition)
        {
            var statue = _context.CreateEntity();
            statue.AddResource(ResourceNames.BlackStatue);
            statue.AddInitialPosition(transformPosition);
            statue.AddCreatureType(CreatureType.BlackStatue);
        }

        public void CreatePosition(Vector3 transformPosition)
        {
            var statue = _context.CreateEntity();
            statue.AddResource(ResourceNames.Position);
            statue.AddInitialPosition(transformPosition);
            statue.AddCreatureType(CreatureType.Position);
        }

        public void CreateBlackBuilding(Vector2 transformPosition)
        {
        
            var statue = _context.CreateEntity();
            statue.AddResource(ResourceNames.BlackBuilding);
            statue.AddInitialPosition(transformPosition);
            statue.AddCreatureType(CreatureType.BlackBuilding);
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
    BlackStatue = 1 << 11,
    BlackWorker = 1 << 12,
    WhiteBuilding = 1 << 13,
    BlackBuilding = 1 << 14,
    Position = 1 << 15
}
