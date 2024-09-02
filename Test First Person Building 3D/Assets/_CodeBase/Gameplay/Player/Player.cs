using _CodeBase.Gameplay.Building;
using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player
{
    [RequireComponent(typeof(LookAround), typeof(Mover), typeof(Raycaster))]
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public Transform PickUpPoint { get; private set; }
        
        private BuildRotator _buildRotator;
        private BuildSnapping _buildSnapping;
        private BuildPlacer _buildPlacer;
        private BuildValidator _buildValidator;
        private BuildMaterialChanger _buildMaterialChanger;
        private BuildPickUp _buildPickUp;
        private IInputService _inputService;

        public void Construct(IInputService inputService,
            BuildRotator buildRotator,
            BuildSnapping buildSnapping,
            BuildPlacer buildPlacer,
            BuildValidator buildValidator,
            BuildMaterialChanger buildMaterialChanger, 
            BuildPickUp buildPickUp)
        {
            _inputService = inputService;
            _buildRotator = buildRotator;
            _buildSnapping = buildSnapping;
            _buildPlacer = buildPlacer;
            _buildValidator = buildValidator;
            _buildMaterialChanger = buildMaterialChanger;
            _buildPickUp = buildPickUp;
            
            _inputService.RotateAwayPerformed +=  _buildRotator.RotateAway;
            _inputService.RotateTowardPerformed += _buildRotator.RotateTowards;
            
            _inputService.PickUpPressed += OnInteractionInput;

            Raycaster.CurrentTargetChanged += _buildSnapping.OnRaycasterTargetChanged;
            _buildPickUp.ActiveBuildableChanged += _buildRotator.ResetRotationAngle;
            _buildPickUp.ActiveBuildableChanged += _buildMaterialChanger.SetBuildableOriginalMaterial;
        }

        public LookAround LookAround { get; private set; }

        public Mover Mover { get; private set; }

        public Raycaster Raycaster { get; private set; }

        private void Awake()
        {
            LookAround = GetComponent<LookAround>();
            Mover = GetComponent<Mover>();
            Raycaster = GetComponent<Raycaster>();
        }

        private void Update()
        {
            _buildSnapping.Update();
            _buildMaterialChanger.Update();
        }

        private void OnDestroy()
        {
            _inputService.RotateAwayPerformed -= _buildRotator.RotateAway;
            _inputService.RotateTowardPerformed -= _buildRotator.RotateTowards;
            
            _inputService.PickUpPressed -= OnInteractionInput;

            Raycaster.CurrentTargetChanged -= _buildSnapping.OnRaycasterTargetChanged;
            _buildPickUp.ActiveBuildableChanged -= _buildRotator.ResetRotationAngle;
            _buildPickUp.ActiveBuildableChanged -= _buildMaterialChanger.SetBuildableOriginalMaterial;
        }

        private void OnInteractionInput()
        {
            if (_buildPickUp.ActiveBuildable == null)
                _buildPickUp.PickUp();
            else
                _buildPlacer.Place();
        }
    }
}