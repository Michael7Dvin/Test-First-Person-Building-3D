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

            _pickUpInteraction.CurrentPickUpableChanged += ResetRotation;
        }

        public float CurrentRotationOffset { get; private set; }
        
        private void ResetRotation(PickUpable pickUpable)
        {
            CurrentRotationOffset = 0;
        }

        public void RotateAway()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;

            CurrentRotationOffset -= _playerConfig.RotationAnglePerInput;
        }

        public  void RotateTowards()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;
            
            CurrentRotationOffset += _playerConfig.RotationAnglePerInput;
        }
    }
}