using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(LookAround), typeof(Mover), typeof(Raycaster))]
    [RequireComponent(typeof(PlayerBuilding))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public Transform PickUpPoint { get; private set; }
        
        public LookAround LookAround { get; private set; }
        public Mover Mover { get; private set; }
        public Raycaster Raycaster { get; private set; }
        public PlayerBuilding PlayerBuilding { get; private set; }

        private void Awake()
        {
            LookAround = GetComponent<LookAround>();
            Mover = GetComponent<Mover>();
            Raycaster = GetComponent<Raycaster>();
            PlayerBuilding = GetComponent<PlayerBuilding>();
        }

        
    }
}