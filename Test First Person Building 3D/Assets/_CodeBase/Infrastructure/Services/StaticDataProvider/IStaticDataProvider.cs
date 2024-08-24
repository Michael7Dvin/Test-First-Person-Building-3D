using _CodeBase.Infrastructure.Services.SceneLoader.Service;

namespace _CodeBase.Infrastructure.Services.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        public ScenesAddresses ScenesAddresses { get; }
    }
}