using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildRotator
    {
        private readonly PlayerConfig _playerConfig;
        private readonly PickUpInteraction _pickUpInteraction;

        public BuildRotator(PlayerConfig playerConfig, PickUpInteraction pickUpInteraction)
        {
            _playerConfig = playerConfig;
            _pickUpInteraction = pickUpInteraction;
        }

        public void RotateAway()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;
            
            _pickUpInteraction.CurrentPickUpable.transform.Rotate(Vector3.up, -_playerConfig.RotationAnglePerInput);
        }

        public  void RotateTowards()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;
            
            _pickUpInteraction.CurrentPickUpable.transform.Rotate(Vector3.up, _playerConfig.RotationAnglePerInput);
        }
    }
}