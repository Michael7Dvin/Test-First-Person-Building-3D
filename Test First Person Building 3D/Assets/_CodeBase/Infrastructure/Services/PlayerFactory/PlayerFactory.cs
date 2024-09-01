using _CodeBase.Gameplay.Building;
using _CodeBase.Gameplay.Player;
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

            float maxRaycastRange = Mathf.Max(_playerConfig.MaxPickUpDistance, _playerConfig.MaxSnappingDistance);
            player.Raycaster.Construct(maxRaycastRange);
            
            player.PickUpInteraction.Construct(_inputService, _playerConfig.MaxPickUpDistance);
            
            BuildRotator buildRotator = new(_playerConfig, player.PickUpInteraction);
            BuildSnapping buildSnapping = new(player.Raycaster, player.PickUpInteraction, buildRotator, player.Camera.transform);
            
            player.Construct(_inputService, buildRotator, buildSnapping);
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