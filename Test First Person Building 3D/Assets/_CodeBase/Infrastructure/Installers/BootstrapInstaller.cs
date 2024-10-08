﻿using _CodeBase.Infrastructure.Bootstrappers;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootstrapper();
        }

        private void BindBootstrapper()
        {
            Container.BindInterfacesTo<AppBootstrapper>().AsSingle();
        }
    }
}