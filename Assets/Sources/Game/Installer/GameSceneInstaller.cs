using Core.Contexts;
using Core.Installer;
using UnityEngine;

namespace Game.Installer
{
    public sealed class GameSceneInstaller : EcsInstaller
    {
        [SerializeField] private Camera mainCamera;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
            Container.Bind<IInputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            InstallSystems();
            base.InstallBindings();
        }

        private void InstallSystems()
        {
//            InstallUpdateSystem<StopSystem>();
//            InstallCommonSystem<InitSystem>();
        }
    }
}