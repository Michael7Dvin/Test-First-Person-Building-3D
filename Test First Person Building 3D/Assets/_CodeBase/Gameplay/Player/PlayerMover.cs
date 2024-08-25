using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private CharacterController _characterController;

        private float _moveSpeed;
        
        public void Construct(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 horizontalDirection)
        {
            _characterController.Move(horizontalDirection * _moveSpeed * Time.deltaTime);
        }
    }
}