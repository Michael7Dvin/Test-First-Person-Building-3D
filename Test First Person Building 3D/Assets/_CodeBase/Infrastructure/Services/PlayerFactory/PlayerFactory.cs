﻿using _CodeBase.Gameplay.Building;
using _CodeBase.Gameplay.Player;
using _CodeBase.Gameplay.Player.Building;
using _CodeBase.Infrastructure.Services.AddressablesLoader;
using _CodeBase.Infrastructure.Services.InputService;
using _CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Services.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly PrefabAddresses _prefabAddresses;
        private readonly PlayerConfig _playerConfig;
        private readonly IInputService _inputService;
        
        private GameObject _playerPrefab;

        public PlayerFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddresses prefabAddresses,
            PlayerConfig playerConfig,
            IInputService inputService)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;
            _prefabAddresses = prefabAddresses;
            _playerConfig = playerConfig;
            _inputService = inputService;
        }

        public async UniTask WarmUpAsync() => 
            _playerPrefab = await _addressablesLoader.LoadGameObjectAsync(_prefabAddresses.Player);

        public void Create(Vector3 position, Quaternion rotation)
        {
            ValidateWarmUpping().Forget();

            Player player = _instantiator.InstantiatePrefabForComponent<Player>(_playerPrefab, position, rotation, null);
            player.LookAround.Construct(_inputService);
            player.Mover.Construct(_inputService, _playerConfig.PlayerSpeed);

            ConstructRaycaster(player);

            ConstructPlayerBuilding(player);
        }

        private void ConstructRaycaster(Player player)
        {
            float maxRaycastRange = Mathf.Max(_playerConfig.MaxPickUpDistance, _playerConfig.MaxSnappingDistance);
            player.Raycaster.Construct(maxRaycastRange);
        }

        private void ConstructPlayerBuilding(Player player)
        {
            BuildPickUp buildPickUp = new(player.Raycaster, _playerConfig.MaxPickUpDistance);
            BuildRotator buildRotator = new(_playerConfig, buildPickUp);
            
            BuildSnapping buildSnapping = 
                new(player.Raycaster, buildPickUp, buildRotator, player.Camera.transform, player.PickUpPoint);
            
            BuildValidator buildValidator = new(buildPickUp, buildSnapping);
            
            BuildMaterialChanger buildMaterialChanger = 
                new(buildPickUp, buildValidator, _playerConfig.ValidPlacementMaterial, _playerConfig.InvalidPlacementMaterial);
            
            BuildPlacer buildPlacer = new(buildPickUp, buildValidator, buildMaterialChanger);

            player.PlayerBuilding.Construct(_inputService, player.Raycaster, buildRotator, buildSnapping, buildPlacer,
                buildValidator, buildMaterialChanger, buildPickUp);
            
            player.PlayerBuilding.Initialize();
        }


        private async UniTaskVoid ValidateWarmUpping()
        {
            if (_playerPrefab == null)
            {
                Debug.LogError($"{nameof(PlayerFactory)} is not warmed up. Call {nameof(WarmUpAsync)} before using factory.");
                await WarmUpAsync();
            }
        }
    }
}