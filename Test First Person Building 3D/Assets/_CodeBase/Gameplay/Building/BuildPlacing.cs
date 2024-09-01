namespace _CodeBase.Gameplay.Building
{
    public class BuildPlacing
    {
        private readonly PickUpInteraction _pickUpInteraction;
        private readonly BuildPlacementValidator _buildPlacementValidator;
        private readonly BuildMaterialChanger _buildMaterialChanger;

        public BuildPlacing(PickUpInteraction pickUpInteraction,
            BuildPlacementValidator buildPlacementValidator,
            BuildMaterialChanger buildMaterialChanger)
        {
            _pickUpInteraction = pickUpInteraction;
            _buildPlacementValidator = buildPlacementValidator;
            _buildMaterialChanger = buildMaterialChanger;
        }
        
        public void Place()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;

            if (_buildPlacementValidator.CanPlace() == true)
            {
                _buildMaterialChanger.RestoreOriginalMaterial();
                _pickUpInteraction.LetGo();
            }
        }
    }
}