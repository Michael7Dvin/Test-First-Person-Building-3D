using System;
using UnityEngine;

namespace _CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService
    {
        Vector2 PlayerLookRotation { get; }
        Vector3 PlayerMoveDirection { get; }

        event Action Interaction;
        event Action RotateToward;
        event Action RotateAway;
        
        void Initialize();
    }
}