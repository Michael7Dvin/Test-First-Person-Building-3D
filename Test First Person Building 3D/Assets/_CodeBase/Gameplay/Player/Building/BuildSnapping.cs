using _CodeBase.Gameplay.Building;
using UnityEngine;

namespace _CodeBase.Gameplay.Player.Building
{
    public class BuildSnapping
    {
        private readonly Raycaster _raycaster;
        private readonly BuildPickUp _buildPickUp;
        private readonly BuildRotator _buildRotator;
        private readonly Transform _cameraTransform;
        private readonly Transform _pickUpPoint;

        public BuildSnapping(Raycaster raycaster,
            BuildPickUp buildPickUp,
            BuildRotator buildRotator,
            Transform cameraTransform,
            Transform pickUpPoint)
        {
            _raycaster = raycaster;
            _buildPickUp = buildPickUp;
            _buildRotator = buildRotator;
            _cameraTransform = cameraTransform;
            _pickUpPoint = pickUpPoint;
        }

        public bool IsBuildZoneTargeted { get; private set; }
        public BuildZone TargetedBuildZone { get; private set; }
        public bool IsBuildableSnapping { get; private set; }

        public void Update()
        {
            if (_buildPickUp.HaveActiveBuildable == false)
            {
                IsBuildableSnapping = false;
                return;
            }

            Buildable buildable = _buildPickUp.ActiveBuildable;
            Transform buildableTransform = buildable.transform;

            bool canSnapBuildableToBuildZone = IsBuildZoneTargeted == true &&
                                               TargetedBuildZone.BuildingAllowed == true &&
                                               (buildable.AllowedBuildZone & TargetedBuildZone.BuildZoneType) != 0;

            if (canSnapBuildableToBuildZone == false)
            {
                IsBuildableSnapping = false;
                
                buildableTransform.position = _pickUpPoint.position;
                RotateToCamera(buildableTransform);
            }
            else
            {
                IsBuildableSnapping = true;

                buildableTransform.position = _raycaster.HitPoint;
                AlignWithBuildZoneSurface(buildableTransform);
            }
        }

        public void OnRaycasterTargetChanged()
        {
            if (_raycaster.HasTarget == false)
            {
                IsBuildZoneTargeted = false;
                TargetedBuildZone = null;
                return;
            }
            
            if (_raycaster.Target.TryGetComponent(out BuildZone buildZone))
            {
                TargetedBuildZone = buildZone;
                IsBuildZoneTargeted = true;
            }
            else
            {
                IsBuildZoneTargeted = false;
                TargetedBuildZone = null;
            }
        }

        private void RotateToCamera(Transform buildableTransform)
        {
            Vector3 directionToCamera = _cameraTransform.position - buildableTransform.position;
            directionToCamera.y = 0;

            Quaternion lookAtCameraRotation = Quaternion.LookRotation(directionToCamera);
            Quaternion finalRotation = lookAtCameraRotation * Quaternion.Euler(0, _buildRotator.RotationAngle, 0);

            buildableTransform.rotation = finalRotation;
        }

        private void AlignWithBuildZoneSurface(Transform buildableTransform)
        {
            Vector3 surfaceNormal = _raycaster.HitNormal;

            bool isWall = Mathf.Abs(surfaceNormal.y) < 0.5f;
            Vector3 forwardDirection;

            if (isWall)
                forwardDirection = Vector3.ProjectOnPlane(_cameraTransform.right, surfaceNormal).normalized;
            else
                forwardDirection = Vector3.ProjectOnPlane(_cameraTransform.forward, surfaceNormal).normalized;

            if (forwardDirection == Vector3.zero)
                forwardDirection = Vector3.ProjectOnPlane(Vector3.forward, surfaceNormal).normalized;

            Quaternion surfaceLookAtRotation = Quaternion.LookRotation(forwardDirection, surfaceNormal);
            Quaternion rotatorRotation = Quaternion.Euler(Vector3.up * _buildRotator.RotationAngle);

            buildableTransform.rotation = surfaceLookAtRotation * rotatorRotation;
        }
    }
}