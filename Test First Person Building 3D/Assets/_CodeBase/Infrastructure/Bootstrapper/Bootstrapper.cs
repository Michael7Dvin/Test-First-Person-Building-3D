using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace _CodeBase.Infrastructure.Bootstrapper
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public Bootstrapper(IGameStateMachine gameStateMachine,
            InitializationState initializationState,
            GameplayState gameplayState,
            SceneLoadingState sceneLoadingState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(gameplayState);
            _gameStateMachine.AddState(sceneLoadingState);
        }

        public void Initialize()
        {
            _gameStateMachine.EnterState<InitializationState>();
        }
    }
}