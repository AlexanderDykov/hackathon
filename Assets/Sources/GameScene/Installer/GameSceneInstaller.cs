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
        private void Awake()
        {
        }

        [SerializeField] private SelectPanel _selectPanel;
        public override void InstallBindings()
        {
            Container.Bind<IGameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
            Container.Bind<IInputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IBoxFactory>().To<BoxFactory>().AsSingle();
            Container.Bind<UIFactory>().AsSingle();

            Container.Bind<SelectPanel>().FromInstance(_selectPanel);
            
            Container.Bind<RandomPositionGenerator>().AsSingle();
            
            InstallUpdateSystem<DestroySystem>();
            InstallCommonSystem<InitSystem>();
            InstallUpdateSystem<PlayerInputSystem>();
            InstallUpdateSystem<AddViewSystem>();
            InstallUpdateSystem<CheckEndGameSystem>();
            InstallUpdateSystem<AddParentSystem>();
            InstallUpdateSystem<GameEventSystems>();
            InstallUpdateSystem<LifeTickSystem>();
            InstallUpdateSystem<SetInitialWorldPositionSystem>();
            InstallUpdateSystem<AddBodySystem>();
            InstallUpdateSystem<PlayerMovementSystem>();
            InstallUpdateSystem<AddAnimatorSystem>();
            
            InstallUpdateSystem<CreateSoulSkillSystem>();
            InstallUpdateSystem<DestroyBoxSystem>();
            InstallUpdateSystem<ShowPanelSystem>();
            base.InstallBindings();
        }
    }
}
