using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _CodeBase.Infrastructure.Services.SceneLoader.Service
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/Scenes", fileName = "SceneAddresses")]
    public class ScenesAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReference Level { get; private set; }
    }
}