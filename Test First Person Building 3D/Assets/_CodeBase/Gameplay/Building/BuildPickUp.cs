using System;
using _CodeBase.Gameplay.Player;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildPickUp
    {
        private readonly Raycaster _raycaster;
        private readonly float _maxPickUpDistance;

        private int _buildableOriginalLayer;
        private LayerMask _buildableColliderOriginalExcludeLayers;

        public BuildPickUp(Raycaster raycaster, float maxPickUpDistance)
        {
            _raycaster = raycaster;
            _maxPickUpDistance = maxPickUpDistance;
        }

        public Buildable ActiveBuildable { get; private set;}
        public bool HaveActiveBuildable { get; private set; }

        public event Action<Buildable> ActiveBuildableChanged; 

        public void PickUp()
        {
            if (_raycaster.HasTarget == false || _raycaster.TargetDistance > _maxPickUpDistance)
                return;

            if (_raycaster.Target.TryGetComponent(out Buildable pickUpable))
            {
                _buildableOriginalLayer = pickUpable.gameObject.layer;
                pickUpable.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                _buildableColliderOriginalExcludeLayers = pickUpable.Collider.excludeLayers;
                pickUpable.Collider.excludeLayers = 1 << LayerMask.NameToLayer("Player");
                
                ActiveBuildable = pickUpable;
                HaveActiveBuildable = true;
                ActiveBuildableChanged?.Invoke(pickUpable);
            }
        }
        
        public void Release()
        {
            ActiveBuildable.gameObject.layer = _buildableOriginalLayer;
            ActiveBuildable.Collider.excludeLayers = _buildableColliderOriginalExcludeLayers;
            
            ActiveBuildable = null;
            HaveActiveBuildable = false;
            ActiveBuildableChanged?.Invoke(null);
        }
    }
}