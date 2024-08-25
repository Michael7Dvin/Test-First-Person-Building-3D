using UnityEngine;
using Zenject;
using InputSettings = _CodeBase.StaticData.InputSettings;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService, ITickable
    {
        private readonly InputSystem_Actions _inputSystemActions = new();
        private readonly float _mouseSensitivity;

        public InputService(InputSettings inputSettings)
        {
            _mouseSensitivity = inputSettings.MouseSensitivity;
        }

        public Vector2 PlayerLookRotation { get; private set; }
        public Vector3 PlayerMoveDirection { get; private set; }

        public void Enable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputSystemActions.Enable();
        }

        public void Tick()
        {
            ReadLookRotation();
            ReadMoveDirection();
        }

        private void ReadLookRotation()
        {
            PlayerLookRotation = _inputSystemActions.Player.Look.ReadValue<Vector2>() * _mouseSensitivity;
        }

        private void ReadMoveDirection()
        {
            Vector2 inputDirection = _inputSystemActions.Player.Move.ReadValue<Vector2>();
            PlayerMoveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
        }
    }
}