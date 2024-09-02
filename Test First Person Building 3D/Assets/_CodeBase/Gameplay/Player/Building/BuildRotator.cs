using _CodeBase.StaticData;

namespace _CodeBase.Gameplay.Building
{
    public class BuildRotator
    {
        private readonly PlayerConfig _playerConfig;
        private readonly BuildPickUp _buildPickUp;

        public BuildRotator(PlayerConfig playerConfig, BuildPickUp buildPickUp)
        {
            _playerConfig = playerConfig;
            _buildPickUp = buildPickUp;
        }

        public float RotationAngle { get; private set; }
        
        public void ResetRotationAngle(Buildable buildable) => 
            RotationAngle = 0;

        public void RotateAway() => 
            Rotate(false);

        public void RotateTowards() => 
            Rotate(true);

        private void Rotate(bool isRotationAnglePositive)
        {
            if (_buildPickUp.HaveActiveBuildable == false)
                return;

            float rotationAngle = _playerConfig.RotationAnglePerInput * (isRotationAnglePositive ? 1 : -1);
            
            RotationAngle += rotationAngle;
        }
    }
}