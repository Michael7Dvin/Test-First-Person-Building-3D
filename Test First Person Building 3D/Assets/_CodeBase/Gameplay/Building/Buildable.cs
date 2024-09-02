using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    [RequireComponent(typeof(Collider), typeof(Renderer))]
    public class Buildable : MonoBehaviour
    {
        [field: SerializeField] public BuildZoneType AllowedBuildZone { get; private set; }

        public Collider Collider { get; private set; }
        public Renderer Renderer { get; private set; }
        
        private void Awake()
        {
            Collider = GetComponent<Collider>();
            Renderer = GetComponent<Renderer>();
        }
    }
}