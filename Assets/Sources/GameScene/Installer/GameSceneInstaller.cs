using Core.Contexts;
using Core.Installer;
using GameScene.ECS.Systems;
using SpaceWars.GameScene.Factories;

namespace GameScene.Installer
{
    public sealed class GameSceneInstaller : EcsInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<IInputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            InstallCommonSystem<InitSystem>();
            InstallUpdateSystem<PlayerInputSystem>();
            InstallUpdateSystem<AddViewSystem>();
            InstallUpdateSystem<AddBodySystem>();
            InstallUpdateSystem<PlayerMovementSystem>();
            InstallUpdateSystem<AddAnimatorSystem>();
            InstallUpdateSystem<PlayerAnimationSystem>();
            base.InstallBindings();
        }
    }
}