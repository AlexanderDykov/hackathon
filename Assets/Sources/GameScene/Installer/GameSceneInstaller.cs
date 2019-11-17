using System;
using System.ComponentModel;
using Core.Contexts;
using Core.Installer;
using Entitas;
using GameScene.ECS;
using GameScene.ECS.Systems;
using GameScene.ECS.Systems.Skill;
using GameScene.ECS.Utils;
using GameScene.Factories;
using GameScene.View;
using UnityEngine;

namespace GameScene.Installer
{
    public sealed class GameSceneInstaller : EcsInstaller
    {
        [SerializeField] private SelectPanel _selectPanel;
        [SerializeField] private Grid _grid;

        public override void InstallBindings()
        {
            Container.Bind<IGameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
            Container.Bind<IInputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IBoxFactory>().To<BoxFactory>().AsSingle();
            Container.Bind<UIFactory>().AsSingle();
            Container.Bind<MonsterFactory>().AsSingle();
            Container.Bind<Grid>().FromInstance(_grid).AsSingle();

            Container.Bind<SelectPanel>().FromInstance(_selectPanel);

            Container.Bind<RandomPositionGenerator>().AsSingle();

            InstallUpdateSystem<DestroySystem>();
            InstallCommonSystem<InitSystem>();
            InstallCommonSystem<InitWorldSystem>();
            
            
            InstallUpdateSystem<TrackCellReputationSystem>();
            InstallUpdateSystem<CreateTileSystem>();
            InstallUpdateSystem<PlayerInputSystem>();
            InstallUpdateSystem<AddViewSystem>();
            InstallUpdateSystem<CheckEndGameSystem>();
            InstallUpdateSystem<AddParentSystem>();
            InstallUpdateSystem<GameEventSystems>();
            InstallUpdateSystem<UpdateBalanceSystem>();
            InstallUpdateSystem<SetInitialWorldPositionSystem>();
            InstallUpdateSystem<AddBodySystem>();
            InstallUpdateSystem<MovementSystem>();
            InstallUpdateSystem<FindTargetSystem>();
            InstallUpdateSystem<AddAnimatorSystem>();
            InstallUpdateSystem<CheckHPSystem>();
            InstallUpdateSystem<DestroyBoxSystem>();
            InstallUpdateSystem<ShowPanelSystem>();
            InstallUpdateSystem<AttackSystem>();
            InstallUpdateSystem<HealSystem>();
            InstallUpdateSystem<CreateNewCreatureSystem>();
            InstallUpdateSystem<CreateSoulBySkillSystem>();
            InstallUpdateSystem<CreateStatueBySkillSystem>();
            InstallUpdateSystem<SoulMoveSystem>();
            InstallUpdateSystem<UpgradeSystem>();
            InstallUpdateSystem<CallDownSystem>();
            InstallUpdateSystem<ZombieCooldownSystem>();
            InstallUpdateSystem<ZombieSystem>();
            InstallUpdateSystem<OpenChestSystem>();
            base.InstallBindings();
        }
    }
}
