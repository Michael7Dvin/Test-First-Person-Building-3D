using UnityEngine;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        Vector2 PlayerCameraLook { get; }

        void Enable();
    }
}