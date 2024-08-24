using _CodeBase.Infrastructure.Services.PlayerFactory;
using _CodeBase.Infrastructure.Services.StaticDataProvider;
using _CodeBase.Infrastructure.StateMachine.States.Base;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class WorldSpawningState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayerFactory _playerFactory;
        
        public void Enter()
        {
        }
    }
}