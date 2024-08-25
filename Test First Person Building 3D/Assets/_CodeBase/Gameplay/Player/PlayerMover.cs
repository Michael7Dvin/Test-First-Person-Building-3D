using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private CharacterController _characterController;
        private IInputService _inputService;

        private Vector3 _inputMoveDirection;
        private float _moveSpeed;

        public void Construct(IInputService inputService, float moveSpeed)
        {
            _inputService = inputService;
            _moveSpeed = moveSpeed;
            
            _inputService.PlayerMoveDirection += ChangeMoveDirection;
        }

        private void Awake() => 
            _characterController = GetComponent<CharacterController>();

        private void Update() => 
            Move();

        private void OnDestroy() => 
            _inputService.PlayerMoveDirection -= ChangeMoveDirection;

        private void ChangeMoveDirection(Vector3 inputMoveDirection) => 
            _inputMoveDirection = inputMoveDirection;

        private  void Move()
        {
            Vector3 bodyAlignedDirection = transform.TransformDirection(_inputMoveDirection);
            Vector3 velocity = bodyAlignedDirection * _moveSpeed;
            velocity.y = Physics.gravity.y;
            
            _characterController.Move(velocity * Time.deltaTime);
        }
    }
}