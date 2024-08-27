using _CodeBase.Infrastructure.Services.AddressablesLoader;
using _CodeBase.Infrastructure.Services.InputService;
using _CodeBase.Infrastructure.Services.PlayerFactory;
using _CodeBase.Infrastructure.Services.SceneLoader.Service;
using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
        }
        
        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<SceneLoadingState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}