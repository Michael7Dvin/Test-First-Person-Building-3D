using _CodeBase.StaticData;

namespace _CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        public ScenesAddresses ScenesAddresses { get; }
        public PrefabAddresses PrefabAddresses { get; }
        
        public RoomLevelConfig RoomLevelConfig { get; }
    }
}