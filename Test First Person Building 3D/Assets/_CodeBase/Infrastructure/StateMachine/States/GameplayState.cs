using _CodeBase.Infrastructure.Services.InputService;
using _CodeBase.Infrastructure.StateMachine.States.Base;

namespace _CodeBase.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly IInputService _inputService;

        public GameplayState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.Enable();
        }
    }
}