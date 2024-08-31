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

        public Transform Target { get; private set;}
        public RaycastHit Hit { get; private set; }
        
        public float TargetDistance => Hit.distance;
        public Vector3 HitPoint => Hit.point;
        public Vector3 HitNormal => Hit.normal;
        
        public event Action CurrentTargetChanged;
        
        private void Update()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, _raycastRange, _mask))
            {
                Hit = hit;
                SetCurrentTarget(hit.transform);
            }
            else
                SetCurrentTarget(null);
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