using System;
using _CodeBase.Gameplay.Building;
using UnityEngine;

namespace _CodeBase.Gameplay.Player.Building
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

            if (_raycaster.Target.TryGetComponent(out Buildable buildable))
            {
                _buildableOriginalLayer = buildable.gameObject.layer;
                buildable.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                
                _buildableColliderOriginalExcludeLayers = buildable.Collider.excludeLayers;
                buildable.Collider.excludeLayers = 1 << LayerMask.NameToLayer("Player");
                
                buildable.DisableChildBuildZones();
                
                _raycaster.IncludeLayer(LayerMask.NameToLayer("BuildZone"));
                
                ActiveBuildable = buildable;
                HaveActiveBuildable = true;
                ActiveBuildableChanged?.Invoke(buildable);
            }
        }
        
        public void Release()
        {
            ActiveBuildable.gameObject.layer = _buildableOriginalLayer;
            ActiveBuildable.Collider.excludeLayers = _buildableColliderOriginalExcludeLayers;
            
            ActiveBuildable.EnableChildBuildZones();

            _raycaster.ExcludeLayer(LayerMask.NameToLayer("BuildZone"));

            ActiveBuildable = null;
            HaveActiveBuildable = false;
            ActiveBuildableChanged?.Invoke(null);
        }
    }
}