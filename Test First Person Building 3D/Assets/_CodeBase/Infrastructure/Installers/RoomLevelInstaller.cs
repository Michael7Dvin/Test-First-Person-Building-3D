using _CodeBase.Infrastructure.Bootstrappers;
using _CodeBase.Infrastructure.Services.PlayerFactory;
using _CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
    public class RoomLevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStates();
            BindServices();
            BindBootstrapper();
        }

        private void BindStates()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<WorldSpawningState>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<GameplayState>().AsSingle();
        }

        private void BindBootstrapper()
        {
            Container.BindInterfacesTo<RoomLevelBootstrapper>().AsSingle();
        }
    }
}