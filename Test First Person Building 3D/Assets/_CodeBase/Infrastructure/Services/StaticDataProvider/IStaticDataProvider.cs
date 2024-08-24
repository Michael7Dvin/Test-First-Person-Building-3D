using _CodeBase.Infrastructure.Services.SceneLoader.Service;
using _CodeBase.StaticData;

namespace _CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        public ScenesAddresses ScenesAddresses { get; }
    }
}