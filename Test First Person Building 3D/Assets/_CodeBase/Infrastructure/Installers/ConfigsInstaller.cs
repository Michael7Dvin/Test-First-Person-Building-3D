using _CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
    public class ConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ScenesAddresses _scenesAddresses; 
        [SerializeField] private PrefabAddresses _prefabAddresses; 
        [SerializeField] private RoomLevelConfig _roomLevelConfig; 

        public override void InstallBindings()
        {
            Container.Bind<ScenesAddresses>().FromInstance(_scenesAddresses).AsSingle();
            Container.Bind<PrefabAddresses>().FromInstance(_prefabAddresses).AsSingle();
            Container.Bind<RoomLevelConfig>().FromInstance(_roomLevelConfig).AsSingle();
        }
    }
}