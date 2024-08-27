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

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObjectAsync(_prefabAddresses.Player);

        public async UniTask Create(Vector3 position, Quaternion rotation)
        {
            GameObject playerPrefab = await _addressablesLoader.LoadGameObjectAsync(_prefabAddresses.Player);
            Player player = _instantiator.InstantiatePrefabForComponent<Player>(playerPrefab, position, rotation, null);
            player.LookAround.Construct(_inputService);
            player.Mover.Construct(_inputService, _playerConfig.PlayerSpeed);

            float maxRaycastRange = Mathf.Max(_playerConfig.MaxPickUpDistance, _playerConfig.MaxSnappingDistance);
            player.Raycaster.Construct(maxRaycastRange);
        }
    }
}