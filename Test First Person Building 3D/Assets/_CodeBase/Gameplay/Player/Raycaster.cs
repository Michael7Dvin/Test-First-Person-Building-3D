using System;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _mask;

        private float _raycastRange;

        public void Construct(float raycastRange)
        {
            _raycastRange = raycastRange;
        }

        public Transform CurrentTarget { get; private set;}
        public float CurrentTargetDistance { get; private set; }
        
        public event Action CurrentTargetChanged;
        
        private void Update()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, _raycastRange, _mask))
            {
                CurrentTargetDistance = hit.distance;
                SetCurrentTarget(hit.transform);
            }
            else
                SetCurrentTarget(null);
        }

        private void SetCurrentTarget(Transform newTarget)
        {
            if (CurrentTarget == newTarget)
                return;
            
            CurrentTarget = newTarget;
            CurrentTargetChanged?.Invoke();
        }
    }
}