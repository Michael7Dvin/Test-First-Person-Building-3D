using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(PlayerLookAround), typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private IInputService _inputService;
        
        private PlayerLookAround _lookAround;
        public PlayerMover Mover { get; private set; }

        [Inject]
        public void InjectServices(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _lookAround = GetComponent<PlayerLookAround>();
            Mover = GetComponent<PlayerMover>();
        }

        private void Update()
        {
            _lookAround.Rotate(_inputService.PlayerCameraLook);
        }
    }
}