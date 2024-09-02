using _CodeBase.Gameplay.Building;
using UnityEngine;

namespace _CodeBase.Gameplay.Player.Building
{
    public class BuildMaterialChanger
    {
        private readonly BuildPickUp _buildPickUp;
        private readonly BuildValidator _buildValidator;
        private readonly Material _validPlacementMaterial;
        private readonly Material _invalidPlacementMaterial;
        
        private Material _originalMaterial;

        public BuildMaterialChanger(BuildPickUp buildPickUp,
            BuildValidator buildValidator,
            Material validPlacementMaterial,
            Material invalidPlacementMaterial)
        {
            _buildPickUp = buildPickUp;
            _buildValidator = buildValidator;
            _validPlacementMaterial = validPlacementMaterial;
            _invalidPlacementMaterial = invalidPlacementMaterial;
        }

        public void SetBuildableOriginalMaterial(Buildable buildable)
        {
            if (_buildPickUp.HaveActiveBuildable == true) 
                _originalMaterial = buildable.Renderer.material;
        }

        public void Update()
        {
            if (_buildPickUp.HaveActiveBuildable == true)
            {
                _buildPickUp.ActiveBuildable.Renderer.material = 
                    _buildValidator.CanPlace() ? _validPlacementMaterial : _invalidPlacementMaterial;
            }
        }

        public void RestoreOriginalMaterial() => 
            _buildPickUp.ActiveBuildable.Renderer.material = _originalMaterial;
    }
}