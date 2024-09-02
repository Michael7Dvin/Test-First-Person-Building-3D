using UnityEngine;

namespace _CodeBase.Gameplay.Player.Building
{
    public class BuildValidator
    {
        private const int OverlapResultsBufferSize = 10;
        
        private readonly BuildPickUp _buildPickUp;
        private readonly BuildSnapping _buildSnapping;
        
        private readonly Collider[] _nonAllocOverlappingColliders = new Collider[OverlapResultsBufferSize];
        public BuildValidator(BuildPickUp buildPickUp, BuildSnapping buildSnapping)
        {
            _buildPickUp = buildPickUp;
            _buildSnapping = buildSnapping;
        }

        public bool CanPlace()
        {
            if (_buildPickUp.HaveActiveBuildable == false || _buildSnapping.IsBuildableSnapping == false)
                return false;

            Collider buildableCollider = _buildPickUp.ActiveBuildable.Collider;

            Bounds bounds = buildableCollider.bounds;
            Vector3 adjustedExtents = bounds.extents * 0.99f;

            int collidersCount = Physics.OverlapBoxNonAlloc(bounds.center,
                adjustedExtents,
                _nonAllocOverlappingColliders,
                buildableCollider.transform.rotation);

            if (IsBufferLimitReached(collidersCount))
            {
                Collider[] allocOverlappingColliders = Physics.OverlapBox(
                    bounds.center,
                    adjustedExtents,
                    buildableCollider.transform.rotation
                );

                return AreOverlapsValid(allocOverlappingColliders, allocOverlappingColliders.Length);
            }

            return AreOverlapsValid(_nonAllocOverlappingColliders, collidersCount);
        }

        private static bool IsBufferLimitReached(int colliderCount) => 
            colliderCount == OverlapResultsBufferSize;
        
        private bool AreOverlapsValid(Collider[] overlappingColliders, int colliderCount)
        {
            for (int i = 0; i < colliderCount; i++)
            {
                Collider collider = overlappingColliders[i];

                if (collider == null)
                    continue;

                if (collider.gameObject == _buildSnapping.TargetedBuildZone.gameObject ||
                    collider.gameObject == _buildPickUp.ActiveBuildable.gameObject)
                {
                    continue;
                }

                Debug.Log(collider.gameObject.name);
                return false;
            }

            return true;
        }
    }
}