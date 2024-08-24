using _CodeBase.Gameplay.Player.CameraLook;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        private IPlayerCameraLook _playerCameraLook;

        private void Update()
        {
            _playerCameraLook.Rotate(Vector2.zero);
        }
    }
}