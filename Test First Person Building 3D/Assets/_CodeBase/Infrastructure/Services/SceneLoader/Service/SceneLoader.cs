using _CodeBase.Infrastructure.Services.AddressablesLoader;
using _CodeBase.Infrastructure.Services.StaticDataProvider;
using _CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.Services.SceneLoader.Service
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IAddressablesLoader _addressablesLoader;
        private readonly ScenesAddresses _scenes;

        public SceneLoader(IAddressablesLoader addressablesLoader, IStaticDataProvider staticDataProvider)
        {
            _addressablesLoader = addressablesLoader;
            _scenes = staticDataProvider.ScenesAddresses;
        }

        public Scene CurrentScene { get; private set; }

        public async UniTask Load(SceneID id)
        {
            switch (id)
            {
                case SceneID.Room:
                    await Load(_scenes.Level);
                    break;
                default:
                    Debug.LogError($"Unable to load scene. Unsupported {nameof(SceneID)}: '{id}'");
                    break;
            }    
        }

        private async UniTask Load(AssetReference sceneReference)
        {
            CurrentScene = await _addressablesLoader.LoadSceneAsync(sceneReference);
            Debug.Log($"Scene loaded: '{CurrentScene.name}'");
        }
    }
}