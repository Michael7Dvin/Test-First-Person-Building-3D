using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(PlayerLook), typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        public PlayerLook Look { get; private set; }
        public PlayerMover Mover { get; private set; }
        
        private void Awake()
        {
            Look = GetComponent<PlayerLook>();
            Mover = GetComponent<PlayerMover>();
        }
    }
}