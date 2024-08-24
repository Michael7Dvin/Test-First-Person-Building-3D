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
            SceneLoadingState sceneLoadingState,
            WorldSpawningState worldSpawningState,
            GameplayState gameplayState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(initializationState);
            _gameStateMachine.AddState(sceneLoadingState);
            _gameStateMachine.AddState(worldSpawningState);
            _gameStateMachine.AddState(gameplayState);
        }

        public void Initialize()
        {
            _gameStateMachine.EnterState<InitializationState>();
        }
    }
}