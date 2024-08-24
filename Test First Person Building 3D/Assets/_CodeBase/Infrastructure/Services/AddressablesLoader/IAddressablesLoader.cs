using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.Services.AddressablesLoader
{
    public interface IAddressablesLoader
    {
        UniTask<GameObject> LoadGameObjectAsync(AssetReferenceGameObject assetReference);
        UniTask<Scene> LoadSceneAsync(AssetReference sceneReference);
    }
}