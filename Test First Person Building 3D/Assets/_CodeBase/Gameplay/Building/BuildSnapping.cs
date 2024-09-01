using _CodeBase.Gameplay.Player;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildSnapping
    {
        private readonly Raycaster _raycaster;
        private readonly PickUpInteraction _pickUpInteraction;
        private readonly BuildRotator _buildRotator;
        private readonly Transform _cameraTransform;
        private readonly Transform _pickUpPoint;
        
        private bool _isBuildZoneTargeted;
        private BuildZone _currentBuildZone;

        public BuildSnapping(Raycaster raycaster,
            PickUpInteraction pickUpInteraction,
            BuildRotator buildRotator,
            Transform cameraTransform,
            Transform pickUpPoint)
        {
            _raycaster = raycaster;
            _pickUpInteraction = pickUpInteraction;
            _buildRotator = buildRotator;
            _cameraTransform = cameraTransform;
            _pickUpPoint = pickUpPoint;
        }

        public void Update()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;
            
            if (_pickUpInteraction.CurrentPickUpable.AllowedBuildZoneType != _currentBuildZone.BuildZoneType || _isBuildZoneTargeted == false)
            {
                _pickUpInteraction.CurrentPickUpable.transform.position = _pickUpPoint.position;
            }
            else
            {
                _pickUpInteraction.CurrentPickUpable.transform.position = _raycaster.HitPoint;

                Quaternion alignedRotation = Quaternion.FromToRotation(Vector3.up, _raycaster.HitNormal);
                _pickUpInteraction.CurrentPickUpable.transform.rotation = alignedRotation;
            }

            Vector3 directionToCamera = _cameraTransform.transform.position - _pickUpInteraction.CurrentPickUpable.transform.position;
        
            directionToCamera.y = 0;

            Quaternion lookAtCameraRotation = Quaternion.LookRotation(directionToCamera);

            Vector3 euler = _pickUpInteraction.CurrentPickUpable.transform.rotation.eulerAngles;
            euler.y = lookAtCameraRotation.eulerAngles.y;
            euler.y += _buildRotator.CurrentRotationOffset;
                
            _pickUpInteraction.CurrentPickUpable.transform.rotation = Quaternion.Euler(euler);
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