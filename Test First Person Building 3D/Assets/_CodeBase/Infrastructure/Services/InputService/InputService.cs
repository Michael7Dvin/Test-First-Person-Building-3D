using System;
using _CodeBase.StaticData;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService, ITickable, IDisposable
    {
        private readonly InputSystem_Actions _inputSystemActions = new();
        private readonly float _mouseSensitivity;

        public InputService(PlayerInputSettings playerInputSettings)
        {
            _mouseSensitivity = playerInputSettings.MouseSensitivity;
        }

        public Vector2 PlayerLookRotation { get; private set; }
        public event Action<Vector3> PlayerMoveDirection;

        public void Enable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputSystemActions.Enable();
            
            _inputSystemActions.Player.Move.started += OnPlayerMoveStarted;
            _inputSystemActions.Player.Move.performed += OnPlayerMoveStarted;
            _inputSystemActions.Player.Move.canceled += OnPlayerMoveStarted;
        }

        public void Tick()
        {
            PlayerLookRotation = _inputSystemActions.Player.Look.ReadValue<Vector2>() * _mouseSensitivity;
        }

        public void Dispose()
        {
            _inputSystemActions.Player.Move.started -= OnPlayerMoveStarted;
            _inputSystemActions.Player.Move.performed -= OnPlayerMoveStarted;
            _inputSystemActions.Player.Move.canceled -= OnPlayerMoveStarted;
            _inputSystemActions?.Dispose();
        }

        private void OnPlayerMoveStarted(InputAction.CallbackContext context)
        {
            Vector2 inputDirection = context.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
            PlayerMoveDirection?.Invoke(moveDirection);
        }
    }
}