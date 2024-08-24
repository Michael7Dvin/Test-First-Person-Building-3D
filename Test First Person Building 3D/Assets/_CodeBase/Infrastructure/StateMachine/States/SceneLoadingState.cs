using _CodeBase.Infrastructure.Services.SceneLoader;
using _CodeBase.Infrastructure.Services.SceneLoader.Service;
using _CodeBase.Infrastructure.StateMachine.States.Base;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class SceneLoadingState : IStateWithArgument<SceneID>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public SceneLoadingState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter(SceneID sceneID)
        {
            await _sceneLoader.Load(sceneID);
            _gameStateMachine.EnterState<WorldSpawningState>();
        }
    }
}