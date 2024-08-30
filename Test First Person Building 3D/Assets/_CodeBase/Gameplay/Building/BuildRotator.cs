using System;
using _CodeBase.Infrastructure.Services.InputService;
using _CodeBase.StaticData;
using UnityEngine;

namespace _CodeBase.Gameplay.Building
{
    public class BuildRotator : IDisposable
    {
        private readonly IInputService _inputService;
        private readonly PlayerConfig _playerConfig;
        private readonly PickUpInteraction _pickUpInteraction;

        public BuildRotator(IInputService inputService, PlayerConfig playerConfig, PickUpInteraction pickUpInteraction)
        {
            _inputService = inputService;
            _playerConfig = playerConfig;
            _pickUpInteraction = pickUpInteraction;
        }

        public void Initialize()
        {
            _inputService.RotateAwayPerformed += RotateAway;
            _inputService.RotateTowardPerformed += RotateTowards;
        }

        public void Dispose()
        {
            _inputService.RotateAwayPerformed -= RotateAway;
            _inputService.RotateTowardPerformed -= RotateTowards;
        }

        private void RotateAway()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;
            
            _pickUpInteraction.CurrentPickUpable.transform.Rotate(Vector3.up, -_playerConfig.RotationAnglePerInput);
        }

        private void RotateTowards()
        {
            if (_pickUpInteraction.CurrentPickUpable == null)
                return;
            
            _pickUpInteraction.CurrentPickUpable.transform.Rotate(Vector3.up, _playerConfig.RotationAnglePerInput);
        }
    }
}