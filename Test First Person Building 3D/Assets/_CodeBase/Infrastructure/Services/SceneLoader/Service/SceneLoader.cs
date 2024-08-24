using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.Services.SceneLoader.Service
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ScenesAddresses _scenes;

        public SceneLoader(IStaticDataProvider staticDataProvider)
        {
            _scenes = staticDataProvider.AssetsAddresses.Scenes;
        }

        public Scene CurrentScene { get; private set; }

        public async UniTask Load(SceneID id)
        {
            switch (id)
            {
                case SceneID.Level:
                    await Load(_scenes.Level);
                    break;
                default:
                    Debug.LogError($"Unable to Load scene. Unsupported {nameof(SceneID)}: '{id}'");
                    break;
            }    
            
            Debug.Log($"Loaded scene: '{id}'");
        }

        private async UniTask Load(AssetReference sceneReference)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference);
            await handle.Task;
            CurrentScene = handle.Result.Scene;
        }
    }
}