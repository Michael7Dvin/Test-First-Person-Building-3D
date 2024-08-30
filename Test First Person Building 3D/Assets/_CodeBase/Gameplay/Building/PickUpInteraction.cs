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
        
        private void PickUp()
        {
            if (_raycaster.CurrentTarget == null || _raycaster.CurrentTargetDistance > _maxPickUpDistance)
                return;

            if (_raycaster.CurrentTarget.TryGetComponent(out PickUpable pickUpable))
            {
                Transform pickUpableTransform = pickUpable.transform;

                pickUpableTransform.parent = _pickUpPoint;
                pickUpableTransform.localPosition = new Vector3(0, 0, 0);
                pickUpableTransform.localRotation = Quaternion.identity;
                CurrentPickUpable = pickUpable;
            }
        }

        private void OnDestroy()
        {
            _inputService.PickUpPressed -= PickUp;
        }
    }
}