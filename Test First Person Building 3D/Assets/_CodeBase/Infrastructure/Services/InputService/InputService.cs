using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using InputSettings = _CodeBase.StaticData.InputSettings;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService, ITickable, IDisposable
    {
        private readonly InputSystem_Actions _inputSystemActions = new();
        private readonly float _mouseSensitivity;

        public InputService(InputSettings inputSettings)
        {
            _mouseSensitivity = inputSettings.MouseSensitivity;
        }

        public Vector2 PlayerLookRotation { get; private set; }
        public Vector3 PlayerMoveDirection { get; private set; }
        public event Action PickUpPressed;

        public void Enable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputSystemActions.Enable();
            _inputSystemActions.Player.Interact.started += OnInteractPerformed;
        }

        private void OnInteractPerformed(InputAction.CallbackContext _) => 
            PickUpPressed?.Invoke();

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

        public void Dispose()
        {
            _inputSystemActions.Player.Interact.performed -= OnInteractPerformed;
            _inputSystemActions?.Dispose();
        }
    }
}