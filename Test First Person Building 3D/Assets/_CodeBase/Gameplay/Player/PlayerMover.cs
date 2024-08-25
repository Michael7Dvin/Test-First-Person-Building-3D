using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private CharacterController _characterController;

        private Vector3 _inputMoveDirection;
        private float _moveSpeed = 5f;

        public void Construct(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void ChangeMoveDirection(Vector3 inputMoveDirection) => 
            _inputMoveDirection = inputMoveDirection;

        public void Move()
        {
            Vector3 cameraAlignedMoveDirection = transform.TransformDirection(_inputMoveDirection);
            cameraAlignedMoveDirection.y = Physics.gravity.y;
            _characterController.Move(cameraAlignedMoveDirection * (_moveSpeed * Time.deltaTime));
        }
    }
}