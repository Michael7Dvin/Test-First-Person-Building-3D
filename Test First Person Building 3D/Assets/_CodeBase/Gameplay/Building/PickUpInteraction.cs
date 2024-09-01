using System;
using _CodeBase.Gameplay.Player;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class PickUpInteraction : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpPoint;
        [SerializeField] private Raycaster _raycaster;
        
        private float _maxPickUpDistance;

        private int _currentPickUpableOriginalLayer;
        
        public void Construct(float maxPickUpDistance)
        {
            _maxPickUpDistance = maxPickUpDistance;
        }
        
        public PickUpable CurrentPickUpable { get; private set;}
        
        public event Action<PickUpable> CurrentPickUpableChanged; 

        public void PickUp()
        {
            if (_raycaster.Target == null || _raycaster.TargetDistance > _maxPickUpDistance)
                return;

            if (_raycaster.Target.TryGetComponent(out PickUpable pickUpable))
            {
                Transform pickUpableTransform = pickUpable.transform;

                _currentPickUpableOriginalLayer = pickUpable.gameObject.layer;
                
                pickUpableTransform.parent = _pickUpPoint;
                pickUpableTransform.localPosition = new Vector3(0, 0, 0);
                pickUpableTransform.localRotation = Quaternion.identity;
                pickUpable.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                CurrentPickUpable = pickUpable;
                CurrentPickUpableChanged?.Invoke(pickUpable);
            }
        }

        public void LetGo()
        {
            if (CurrentPickUpable == null)
                return;
            
            CurrentPickUpable.transform.parent = null;
            CurrentPickUpable.gameObject.layer = _currentPickUpableOriginalLayer;
            CurrentPickUpable = null;
            CurrentPickUpableChanged?.Invoke(null);
        }
    }
}