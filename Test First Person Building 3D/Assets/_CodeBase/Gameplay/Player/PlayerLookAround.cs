using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    public class PlayerLookAround : MonoBehaviour 
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _bodyTransform;

        public PlayerLookAround(Transform cameraTransform, Transform bodyTransform)
        {
            _cameraTransform = cameraTransform;
            _bodyTransform = bodyTransform;
        }

        private float _rotationX;
        private float _rotationY;
        
        public void Rotate(Vector2 inputLookRotation)
        {      
            _rotationX -= inputLookRotation.y;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            _rotationY = inputLookRotation.x;

            _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
            _bodyTransform.Rotate(Vector3.up * _rotationY);
        }
    }
}