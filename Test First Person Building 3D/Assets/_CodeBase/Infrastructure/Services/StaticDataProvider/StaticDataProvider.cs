using _CodeBase.StaticData;

namespace _CodeBase.Infrastructure.Services.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(ScenesAddresses scenesAddresses,
            PrefabAddresses prefabAddresses,
            RoomLevelConfig roomLevelConfig)
        {
            ScenesAddresses = scenesAddresses;
            PrefabAddresses = prefabAddresses;
            RoomLevelConfig = roomLevelConfig;
        }

        public ScenesAddresses ScenesAddresses { get; }
        public PrefabAddresses PrefabAddresses { get; }
        public RoomLevelConfig RoomLevelConfig { get; }
    }
}