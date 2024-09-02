using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    [RequireComponent(typeof(Collider))]
    public class BuildZone : MonoBehaviour
    {
        [field: SerializeField] public BuildZoneType BuildZoneType { get; private set; }
        [field: SerializeField] public bool BuildingAllowed { get; private set; }

        private Collider _buildZoneCollider;
        
        private void Awake()
        {
            _buildZoneCollider = GetComponent<Collider>();
            AllowBuilding();
        }

        public void AllowBuilding()
        {
            gameObject.layer = LayerMask.NameToLayer("BuildZone");
            _buildZoneCollider.enabled = true;
            BuildingAllowed = true;
        }
        
        public void DisallowBuilding()
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            _buildZoneCollider.enabled = false;
            BuildingAllowed = false;
        }
    }
}