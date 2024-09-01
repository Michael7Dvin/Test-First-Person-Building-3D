using System;
using _CodeBase.Gameplay.Player;
using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class PickUpInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpPoint;
        [SerializeField] private Raycaster _raycaster;
        
        private IInputService _inputService;
        private float _maxPickUpDistance;

        public void Construct(IInputService inputService, float maxPickUpDistance)
        {
            _inputService = inputService;
            _maxPickUpDistance = maxPickUpDistance;
            _inputService.PickUpPressed += PickUp;
        }
        
        public PickUpable CurrentPickUpable { get; private set;}
        
        public event Action<PickUpable> CurrentPickUpableChanged; 

        private void PickUp()
        {
            if (_raycaster.Target == null || _raycaster.TargetDistance > _maxPickUpDistance)
                return;

            if (_raycaster.Target.TryGetComponent(out PickUpable pickUpable))
            {
                Transform pickUpableTransform = pickUpable.transform;

                pickUpableTransform.parent = _pickUpPoint;
                pickUpableTransform.localPosition = new Vector3(0, 0, 0);
                pickUpableTransform.localRotation = Quaternion.identity;
                pickUpable.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                CurrentPickUpable = pickUpable;
                CurrentPickUpableChanged?.Invoke(pickUpable);
            }
        }

        private void OnDestroy()
        {
            _inputService.PickUpPressed -= PickUp;
        }
    }
}