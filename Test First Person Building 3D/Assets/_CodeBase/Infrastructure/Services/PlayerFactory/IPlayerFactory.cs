using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _CodeBase.Infrastructure.Services.PlayerFactory
{
    public interface IPlayerFactory
    {
        UniTask WarmUpAsync();
        void Create(Vector3 position, Quaternion rotation);
    }
}