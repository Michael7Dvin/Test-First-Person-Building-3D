using System;
using System.Numerics;
using _CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace _CodeBase.Infrastructure.Services.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PrefabAddresses _prefabAddresses;

        public UniTask WarmUp()
        {
            throw new NotImplementedException();
        }

        public UniTask Create(Vector3 position, Quaternion rotation)
        {
            throw new NotImplementedException();
        }
    }

    public interface IPlayerFactory
    {
        UniTask WarmUp();
        UniTask Create(Vector3 position, Quaternion rotation);
    }
}