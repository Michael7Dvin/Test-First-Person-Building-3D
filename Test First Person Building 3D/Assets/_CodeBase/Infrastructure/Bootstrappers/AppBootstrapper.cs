using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace _CodeBase.Infrastructure.Bootstrappers
{
    public class AppBootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public AppBootstrapper(IGameStateMachine gameStateMachine,
            InitializationState initializationState,
            SceneLoadingState sceneLoadingState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(sceneLoadingState);
        }

        public void Initialize()
        {
            _gameStateMachine.EnterState<InitializationState>();
        }
    }
}