using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildPlacementValidator
    {
        private readonly PickUpInteraction _pickUpInteraction;

        public BuildPlacementValidator(PickUpInteraction pickUpInteraction)
        {
            _pickUpInteraction = pickUpInteraction;
        }

        public bool CanPlace()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return false;

            Collider pickUpableCollider = _pickUpInteraction.CurrentPickUpable.Collider;
            
            Vector3 adjustedExtents = pickUpableCollider.bounds.extents * 0.99f;

            Collider[] overlappingColliders = Physics.OverlapBox(
                pickUpableCollider.bounds.center,
                adjustedExtents,
                pickUpableCollider.transform.rotation
            );

            foreach (var collider in overlappingColliders)
            {
                if (collider.gameObject != _pickUpInteraction.CurrentPickUpable.gameObject)
                {
                    return false; 
                }
            }

            return true;
        }
    }
}