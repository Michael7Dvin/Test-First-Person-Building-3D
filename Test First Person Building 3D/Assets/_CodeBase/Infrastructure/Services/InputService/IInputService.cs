using System;
using UnityEngine;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        Vector2 PlayerLookRotation { get; }
        event Action<Vector3> PlayerMoveDirection;  

        void Enable();
    }
}