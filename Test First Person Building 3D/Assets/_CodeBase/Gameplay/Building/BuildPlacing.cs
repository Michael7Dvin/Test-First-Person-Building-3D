using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildPlacing
    {
        private readonly PickUpInteraction _pickUpInteraction;

        public BuildPlacing(PickUpInteraction pickUpInteraction)
        {
            _pickUpInteraction = pickUpInteraction;
        }
        
        public void Place()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;

            if (CheckBounds() == true)
            {
                _pickUpInteraction.LetGo();
            }
        }

        private bool CheckBounds()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return false;

            Collider objectCollider = _pickUpInteraction.CurrentPickUpable.GetComponent<Collider>();
            if (objectCollider == null)
                return false;

            Vector3 adjustedExtents = objectCollider.bounds.extents * 0.99f;
            
            Collider[] overlappingColliders = Physics.OverlapBox(
                objectCollider.bounds.center,
                adjustedExtents,
                objectCollider.transform.rotation
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