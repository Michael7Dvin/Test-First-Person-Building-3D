using UnityEngine;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        Vector2 PlayerLookRotation { get; }
        Vector3 PlayerMoveDirection { get; }

        void Enable();
    }
}