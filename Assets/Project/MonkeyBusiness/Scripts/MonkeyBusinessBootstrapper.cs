using MonkeyBusiness.Infrastructure.Factory;
using Zenject;

namespace MonkeyBusiness
{
    public class MonkeyBusinessBootstrapper : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMonkeyBusinessFactory>().To<MonkeyBusinessFactory>().AsSingle();
        }
    }
}
