﻿using _CodeBase.Infrastructure.StateMachine;
using _CodeBase.Infrastructure.StateMachine.States;
using Zenject;

namespace _CodeBase.Infrastructure.Bootstrappers
{
    public class RoomLevelBootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public RoomLevelBootstrapper(IGameStateMachine gameStateMachine,
            WorldSpawningState worldSpawningState,
            GameplayState gameplayState)
        {
            _gameStateMachine = gameStateMachine;
            
            _gameStateMachine.AddState(worldSpawningState);
            _gameStateMachine.AddState(gameplayState);
        }

        public void Initialize()
        {
            _gameStateMachine.EnterState<WorldSpawningState>();
        }
    }
}