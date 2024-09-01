using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildMaterialChanger
    {
        private readonly BuildPlacementValidator _buildPlacementValidator;
        private readonly Material _validPlacementMaterial;
        private readonly Material _invalidPlacementMaterial;
        private Material _originalMaterial;
        private Renderer _renderer;

        public BuildMaterialChanger(BuildPlacementValidator buildPlacementValidator,
            Material validPlacementMaterial,
            Material invalidPlacementMaterial)
        {
            _buildPlacementValidator = buildPlacementValidator;
            _validPlacementMaterial = validPlacementMaterial;
            _invalidPlacementMaterial = invalidPlacementMaterial;
        }

        public void SetPickUpable(PickUpable pickUpable)
        {
            if (pickUpable != null)
            {
                _originalMaterial = pickUpable.Renderer.material;
                _renderer = pickUpable.Renderer;
            }
        }

        public void Update()
        {
            if (_renderer != null)
                _renderer.material = _buildPlacementValidator.CanPlace() ? _validPlacementMaterial : _invalidPlacementMaterial;
        }

        public void RestoreOriginalMaterial()
        {
            if (_renderer != null)
            {
                _renderer.material = _originalMaterial;
                _renderer = null;                   
            }
        }
    }
}