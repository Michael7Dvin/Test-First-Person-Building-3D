using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Addresses/ScenesAddresses", fileName = "ScenesAddresses")]
    public class ScenesAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReference Level { get; private set; }
    }
}