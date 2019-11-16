using Core.Contexts;
using Core.Installer;
using GameScene.ECS.Systems;
using GameScene.ECS.Utils;
using GameScene.Factories;
using GameScene.View;
using UnityEngine;

namespace GameScene.Installer
{
    public sealed class GameSceneInstaller : EcsInstaller
    {
        [SerializeField] private SelectPanel _selectPanel;
        public override void InstallBindings()
        {
            Container.Bind<IGameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<IInputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IBoxFactory>().To<BoxFactory>().AsSingle();

            Container.Bind<SelectPanel>().FromInstance(_selectPanel);
            
            Container.Bind<RandomPositionGenerator>().AsSingle();
            InstallCommonSystem<InitSystem>();
            InstallUpdateSystem<PlayerInputSystem>();
            InstallUpdateSystem<AddViewSystem>();
            InstallUpdateSystem<ShowPanelSystem>();
            InstallUpdateSystem<SetInitialWorldPositionSystem>();
            InstallUpdateSystem<AddBodySystem>();
            InstallUpdateSystem<PlayerMovementSystem>();
            InstallUpdateSystem<AddAnimatorSystem>();
            InstallUpdateSystem<PlayerAnimationSystem>();
            base.InstallBindings();
        }
    }
}