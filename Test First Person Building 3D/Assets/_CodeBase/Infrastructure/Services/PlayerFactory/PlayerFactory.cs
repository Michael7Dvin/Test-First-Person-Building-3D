using _CodeBase.Infrastructure.Services.AddressablesLoader;
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

        public PlayerFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddresses prefabAddresses)
        {
            _instantiator = instantiator;
            _addressablesLoader = addressablesLoader;
            _prefabAddresses = prefabAddresses;
        }

        public async UniTask WarmUp() => 
            await _addressablesLoader.LoadGameObjectAsync(_prefabAddresses.Player);

        public async UniTask Create(Vector3 position, Quaternion rotation)
        {
            GameObject playerPrefab = await _addressablesLoader.LoadGameObjectAsync(_prefabAddresses.Player);
            GameObject playerGameObject = _instantiator.InstantiatePrefab(playerPrefab, position, rotation, null);
        }
    }
}