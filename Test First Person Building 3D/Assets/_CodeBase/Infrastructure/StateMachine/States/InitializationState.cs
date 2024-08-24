using _CodeBase.Infrastructure.Services.SceneLoader;
using _CodeBase.Infrastructure.StateMachine.States.Base;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.Device;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class InitializationState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public InitializationState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await Addressables.InitializeAsync();
            DOTween.Init();
            
            Application.targetFrameRate = 60;
            
            _gameStateMachine.EnterState<SceneLoadingState, SceneID>(SceneID.Level);
        }

        public void Exit()
        {
        }
    }
}