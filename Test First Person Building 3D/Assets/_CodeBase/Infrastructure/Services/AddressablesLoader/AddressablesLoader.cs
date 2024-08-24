using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.Services.AddressablesLoader
{
    public class AddressablesLoader : IAddressablesLoader
    {
        public async UniTask<GameObject> LoadGameObjectAsync(AssetReferenceGameObject assetReference)
        {
            if (assetReference.RuntimeKeyIsValid() == false)
            {
                Debug.LogError("Unable to load GameObject. AssetReference is null");
                return null;
            }

            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
            await handle.Task;
            return handle.Result;
        }

        public async UniTask<Scene> LoadSceneAsync(AssetReference sceneReference)
        {
            if (sceneReference.RuntimeKeyIsValid() == false)
            {
                Debug.LogError("Unable to load Scene. AssetReference is null");
                return default;
            }
            
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference);
            await handle.Task;
            return handle.Result.Scene;
        }
    }
}