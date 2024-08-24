using _CodeBase.Infrastructure.Services.PlayerFactory;
using _CodeBase.Infrastructure.Services.StaticDataProvider;
using _CodeBase.Infrastructure.StateMachine.States.Base;
using _CodeBase.StaticData;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class WorldSpawningState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayerFactory _playerFactory;
        private readonly RoomLevelConfig _roomLevelConfig;

        public WorldSpawningState(IGameStateMachine gameStateMachine,
            IPlayerFactory playerFactory,
            IStaticDataProvider staticDataProvider)
        {
            _gameStateMachine = gameStateMachine;
            _playerFactory = playerFactory;
            _roomLevelConfig = staticDataProvider.RoomLevelConfig;
        }

        public async void Enter()
        {
            await _playerFactory.Create(_roomLevelConfig.PlayerSpawnPosition, _roomLevelConfig.PlayerRotation);
            _gameStateMachine.EnterState<GameplayState>();
        }
    }
}