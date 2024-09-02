namespace _CodeBase.Gameplay.Building
{
    public class BuildPlacer
    {
        private readonly BuildPickUp _buildPickUp;
        private readonly BuildValidator _buildValidator;
        private readonly BuildMaterialChanger _buildMaterialChanger;

        public BuildPlacer(BuildPickUp buildPickUp,
            BuildValidator buildValidator,
            BuildMaterialChanger buildMaterialChanger)
        {
            _buildPickUp = buildPickUp;
            _buildValidator = buildValidator;
            _buildMaterialChanger = buildMaterialChanger;
        }
        
        public void Place()
        {
            if (_buildValidator.CanPlace() == true)
            {
                _buildMaterialChanger.RestoreOriginalMaterial();
                _buildPickUp.Release();
            }
        }
    }
}