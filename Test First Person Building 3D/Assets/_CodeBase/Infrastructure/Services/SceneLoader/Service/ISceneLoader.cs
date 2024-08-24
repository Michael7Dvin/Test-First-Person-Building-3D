using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.Services.SceneLoader.Service
{
    public interface ISceneLoader
    {
        Scene CurrentScene { get; }
        UniTask Load(SceneID id);
    }
}