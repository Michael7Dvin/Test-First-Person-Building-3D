using System;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _mask;

        private float _raycastRange;
        
        private RaycastHit _hit;
        
        public void Construct(float raycastRange)
        {
            _raycastRange = raycastRange;
        }

        public Transform Target { get; private set;}

        public bool HasTarget { get; private set; }
        public float TargetDistance => _hit.distance;
        public Vector3 HitPoint => _hit.point;
        public Vector3 HitNormal => _hit.normal;
        
        public event Action CurrentTargetChanged;
        
        private void Update()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, _raycastRange, _mask))
            {
                _hit = hit;
                HasTarget = true;
                SetCurrentTarget(hit.transform);
            }
            else
            {
                HasTarget = false;
                SetCurrentTarget(null);
            }
        }

        private void SetCurrentTarget(Transform newTarget)
        {
            if (Target == newTarget)
                return;
            
            Target = newTarget;
            CurrentTargetChanged?.Invoke();
        }
    }
}