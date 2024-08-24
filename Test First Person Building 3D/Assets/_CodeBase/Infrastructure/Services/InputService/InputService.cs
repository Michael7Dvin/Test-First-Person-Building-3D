using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService, ITickable
    {
        private readonly InputSystem_Actions _inputSystemActions = new();
        private float _mouseSensivity;

        public Vector2 PlayerCameraLook { get; private set; }

        public void Tick()
        {
            PlayerCameraLook = _inputSystemActions.Player.Look.ReadValue<Vector2>() * _mouseSensivity;
        }
    }
}