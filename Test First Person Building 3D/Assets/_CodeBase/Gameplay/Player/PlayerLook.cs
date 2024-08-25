using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    public class PlayerLook : MonoBehaviour 
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _bodyTransform;

        private IInputService _inputService;
        
        private float _rotationX;
        private float _rotationY;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            Rotate(_inputService.PlayerLookRotation);
        }
        
        private void Rotate(Vector2 inputLookRotation)
        {      
            _rotationX -= inputLookRotation.y;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            _rotationY = inputLookRotation.x;

            _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
            _bodyTransform.Rotate(Vector3.up * _rotationY);
        }
    }
}