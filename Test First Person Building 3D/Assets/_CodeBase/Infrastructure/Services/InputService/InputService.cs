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
        public event Action Interaction;
        public event Action RotateToward;
        public event Action RotateAway;

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputSystemActions.Enable();
            _inputSystemActions.Player.Interact.started += OnInteractInput;

            _inputSystemActions.Player.RotateAway.performed += OnRotateAwayInput;
            _inputSystemActions.Player.RotateToward.performed += OnRotateTowardInput;
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

        private void OnRotateAwayInput(InputAction.CallbackContext obj) => 
            RotateAway?.Invoke();

        private void OnRotateTowardInput(InputAction.CallbackContext obj) => 
            RotateToward?.Invoke();

        private void OnInteractInput(InputAction.CallbackContext _) => 
            Interaction?.Invoke();
        
        public void Dispose()
        {
            _inputSystemActions.Player.Interact.performed -= OnInteractInput;
            _inputSystemActions.Player.RotateAway.performed -= OnRotateAwayInput;
            _inputSystemActions.Player.RotateToward.performed -= OnRotateTowardInput;
            _inputSystemActions?.Dispose();
        }
    }
}