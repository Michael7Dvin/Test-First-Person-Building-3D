using _CodeBase.Gameplay.Player.CameraLook;
using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace _CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        private IInputService _inputService;
        
        private IPlayerCameraLook _playerCameraLook;

        [Inject]
        public void InjectServices(IInputService inputService) => 
            _inputService = inputService;

        public void Construct(IPlayerCameraLook playerCameraLook)
        {
            _playerCameraLook = playerCameraLook;
        }
        
        private void Update()
        {
            _playerCameraLook.Rotate(_inputService.PlayerCameraLook);
        }
    }
}