using UnityEngine;

namespace _CodeBase.Gameplay.Player.CameraLook
{
    public interface IPlayerCameraLook
    {
        void Rotate(Vector2 inputLookRotation);
    }
}