using _CodeBase.Gameplay.Player;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildSnapping
    {
        private readonly Raycaster _raycaster;
        private readonly PickUpInteraction _pickUpInteraction;
        
        private bool _isBuildZoneTargeted;
        private BuildZone _currentBuildZone;

        public BuildSnapping(Raycaster raycaster, PickUpInteraction pickUpInteraction)
        {
            _raycaster = raycaster;
            _pickUpInteraction = pickUpInteraction;
        }

        public void Update()
        {
            if (_isBuildZoneTargeted == false || _pickUpInteraction.CurrentPickUpable == null)
                return;

            if (_pickUpInteraction.CurrentPickUpable.AllowedBuildZoneType == _currentBuildZone.BuildZoneType)
            {
                _pickUpInteraction.CurrentPickUpable.transform.position = _raycaster.HitPoint;

                Quaternion alignedRotation = Quaternion.LookRotation(_raycaster.HitNormal);

                _pickUpInteraction.CurrentPickUpable.transform.rotation = alignedRotation;
            }
        }
        
        public void OnRaycasterTargetChanged()
        {
            if (_raycaster.Target == null)
            {
                _isBuildZoneTargeted = false;
                return;
            }
            
            if (_raycaster.Target.TryGetComponent(out BuildZone buildZone))
            {
                _currentBuildZone = buildZone;
                _isBuildZoneTargeted = true;
            }
            else
                _isBuildZoneTargeted = false;
        }
    }
}