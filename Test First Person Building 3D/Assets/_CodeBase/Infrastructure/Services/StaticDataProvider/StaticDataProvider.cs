using _CodeBase.Infrastructure.Services.SceneLoader.Service;
using _CodeBase.StaticData;

namespace _CodeBase.Infrastructure.Services.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(ScenesAddresses scenesAddresses)
        {
            ScenesAddresses = scenesAddresses;
        }

        public ScenesAddresses ScenesAddresses { get; }
    }
}