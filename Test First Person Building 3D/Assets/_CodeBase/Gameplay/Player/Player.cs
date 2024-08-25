using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(PlayerLook), typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        private IInputService _inputService;
        
        private PlayerLook _look;
        public PlayerMover Mover { get; private set; }

        [Inject]
        public void InjectServices(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _look = GetComponent<PlayerLook>();
            Mover = GetComponent<PlayerMover>();
            
            _inputService.PlayerMoveDirection += Mover.ChangeMoveDirection;
        }

        private void Update()
        {
            _look.Rotate(_inputService.PlayerLookRotation);
            Mover.Move();
        }

        private void OnDestroy()
        {
            _inputService.PlayerMoveDirection -= Mover.ChangeMoveDirection;
        }
    }
}