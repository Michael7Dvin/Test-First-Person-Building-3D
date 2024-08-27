using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Mover : MonoBehaviour
    {
        private CharacterController _characterController;
        private IInputService _inputService;

        private float _moveSpeed;

        public void Construct(IInputService inputService, float moveSpeed)
        {
            _inputService = inputService;
            _moveSpeed = moveSpeed;
        }

        private void Awake() => 
            _characterController = GetComponent<CharacterController>();

        private void Update() => 
            Move();
        
        private void Move()
        {
            Vector3 bodyAlignedDirection = transform.TransformDirection(_inputService.PlayerMoveDirection);
            Vector3 velocity = bodyAlignedDirection * _moveSpeed;
            velocity.y = Physics.gravity.y;
            
            _characterController.Move(velocity * Time.deltaTime);
        }
    }
}