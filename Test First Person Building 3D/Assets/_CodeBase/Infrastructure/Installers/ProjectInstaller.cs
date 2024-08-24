using _CodeBase.Infrastructure.Services.SceneLoader.Service;
using _CodeBase.Infrastructure.Services.StaticDataProvider;
using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ScenesAddresses _scenesAddresses; 
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
        }
        
        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<SceneLoadingState>().AsSingle();
        }

        private void BindServices()
        {
            Container
                .Bind<IStaticDataProvider>()
                .To<StaticDataProvider>()
                .AsSingle()
                .WithArguments(_scenesAddresses);
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}