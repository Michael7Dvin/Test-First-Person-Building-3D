using _CodeBase.Gameplay.Building;
using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(LookAround), typeof(Mover), typeof(Raycaster))]
    [RequireComponent(typeof(PickUpInteraction))]
    public class Player : MonoBehaviour
    {
        private BuildRotator _buildRotator;
        private BuildSnapping _buildSnapping;
        private IInputService _inputService;

        public void Construct(IInputService inputService, BuildRotator buildRotator, BuildSnapping buildSnapping)
        {
            _inputService = inputService;
            _buildRotator = buildRotator;
            _buildSnapping = buildSnapping;
            
            _inputService.RotateAwayPerformed += _buildRotator.RotateAway;
            _inputService.RotateTowardPerformed += _buildRotator.RotateTowards;
            
            Raycaster.CurrentTargetChanged += _buildSnapping.OnRaycasterTargetChanged;
        }
        
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

        private void Update()
        {
            _buildSnapping.Update();
        }

        private void OnDestroy()
        {
            _inputService.RotateAwayPerformed -= _buildRotator.RotateAway;
            _inputService.RotateTowardPerformed -= _buildRotator.RotateTowards;
            
            Raycaster.CurrentTargetChanged -= _buildSnapping.OnRaycasterTargetChanged;
        }
    }
}