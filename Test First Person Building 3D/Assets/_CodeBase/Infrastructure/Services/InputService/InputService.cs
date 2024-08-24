using _CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService, ITickable
    {
        private readonly InputSystem_Actions _inputSystemActions = new();

        private readonly float _mouseSensitivity;

        public InputService(PlayerInputSettings playerInputSettings)
        {
            _mouseSensitivity = playerInputSettings.MouseSensitivity;
        }

        public Vector2 PlayerCameraLook { get; private set; }
        
        public void Enable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputSystemActions.Enable();
        }

        public void Tick()
        {
            PlayerCameraLook = _inputSystemActions.Player.Look.ReadValue<Vector2>() * _mouseSensitivity;
        }
    }
}