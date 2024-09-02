using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    [RequireComponent(typeof(Collider), typeof(Renderer))]
    public class Buildable : MonoBehaviour
    {
        [SerializeField] private BuildZone[] _childrenBuildZones;
        [field: SerializeField] public BuildZoneType AllowedBuildZone { get; private set; }
        
        public Collider Collider { get; private set; }
        public Renderer Renderer { get; private set; }
        
        private void Awake()
        {
            Collider = GetComponent<Collider>();
            Renderer = GetComponent<Renderer>();
        }

        public void EnableChildBuildZones()
        {
            foreach (BuildZone buildZone in _childrenBuildZones) 
                buildZone.AllowBuilding();
        }
        
        public void DisableChildBuildZones()
        {
            foreach (BuildZone buildZone in _childrenBuildZones) 
                buildZone.DisallowBuilding();
        }
    }
}