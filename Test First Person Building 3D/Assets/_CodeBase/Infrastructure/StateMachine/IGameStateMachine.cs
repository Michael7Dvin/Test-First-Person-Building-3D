﻿using _CodeBase.Infrastructure.StateMachine.States.Base;

namespace _CodeBase.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : IState;
        void EnterState<TState, TArgument>(TArgument argument) where TState : IStateWithArgument<TArgument>;
        void AddState<TState>(TState state) where TState : IExitableState;
    }
}