using _CodeBase.Gameplay.Building;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(LookAround), typeof(Mover), typeof(Raycaster))]
    [RequireComponent(typeof(PickUpInteraction))]
    public class Player : MonoBehaviour
    {
        public LookAround LookAround { get; private set; }
        public Mover Mover { get; private set; }
        public Raycaster Raycaster { get; private set; }
        public PickUpInteraction PickUpInteraction { get; private set; }
        
        private void Awake()
        {
            LookAround = GetComponent<LookAround>();
            Mover = GetComponent<Mover>();
            Raycaster = GetComponent<Raycaster>();
            
            PickUpInteraction = GetComponent<PickUpInteraction>();
        }
    }
}