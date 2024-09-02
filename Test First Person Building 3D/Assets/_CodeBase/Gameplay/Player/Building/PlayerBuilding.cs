using _CodeBase.Infrastructure.Services.InputService;
using UnityEngine;

namespace _CodeBase.Gameplay.Player.Building
{
    public class PlayerBuilding : MonoBehaviour
    {
        private IInputService _inputService;
        private Raycaster _raycaster;
        
        private BuildRotator _buildRotator;
        private BuildSnapping _buildSnapping;
        private BuildPlacer _buildPlacer;
        private BuildValidator _buildValidator;
        private BuildMaterialChanger _buildMaterialChanger;
        private BuildPickUp _buildPickUp;

        public void Construct(IInputService inputService,
            Raycaster raycaster,
            BuildRotator buildRotator,
            BuildSnapping buildSnapping,
            BuildPlacer buildPlacer,
            BuildValidator buildValidator,
            BuildMaterialChanger buildMaterialChanger, 
            BuildPickUp buildPickUp)
        {
            _inputService = inputService;
            _raycaster = raycaster;
            
            _buildRotator = buildRotator;
            _buildSnapping = buildSnapping;
            _buildPlacer = buildPlacer;
            _buildValidator = buildValidator;
            _buildMaterialChanger = buildMaterialChanger;
            _buildPickUp = buildPickUp;
        }

        private void Update()
        {
            _buildSnapping.Update();
            _buildMaterialChanger.Update();
        }

        private void OnDestroy()
        {
            _inputService.RotateAway -= _buildRotator.RotateAway;
            _inputService.RotateToward -= _buildRotator.RotateTowards;
            
            _inputService.Interaction -= OnInteractionInput;

            _raycaster.CurrentTargetChanged -= _buildSnapping.OnRaycasterTargetChanged;
            _buildPickUp.ActiveBuildableChanged -= _buildRotator.ResetRotationAngle;
            _buildPickUp.ActiveBuildableChanged -= _buildMaterialChanger.SetBuildableOriginalMaterial;
        }

        public void Initialize()
        {
            _inputService.RotateAway += _buildRotator.RotateAway;
            _inputService.RotateToward += _buildRotator.RotateTowards;

            _inputService.Interaction += OnInteractionInput;

            _raycaster.CurrentTargetChanged += _buildSnapping.OnRaycasterTargetChanged;
            _buildPickUp.ActiveBuildableChanged += _buildRotator.ResetRotationAngle;
            _buildPickUp.ActiveBuildableChanged += _buildMaterialChanger.SetBuildableOriginalMaterial;
        }

        private void OnInteractionInput()
        {
            if (_buildPickUp.HaveActiveBuildable == false)
                _buildPickUp.PickUp();
            else
                _buildPlacer.Place();
        }
    }
}