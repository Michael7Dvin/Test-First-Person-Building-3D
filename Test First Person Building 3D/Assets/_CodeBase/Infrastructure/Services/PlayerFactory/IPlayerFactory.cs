using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _CodeBase.Infrastructure.Services.PlayerFactory
{
    public interface IPlayerFactory
    {
        UniTask WarmUp();
        UniTask Create(Vector3 position, Quaternion rotation);
    }
}