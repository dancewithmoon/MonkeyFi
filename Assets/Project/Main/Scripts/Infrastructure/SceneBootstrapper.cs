using Base.Services;
using Zenject;

namespace Main.Infrastructure
{
    public class SceneBootstrapper : MonoInstaller
    {
        [Inject] public ContainerHolder ContainerHolder;
        
        public override void InstallBindings()
        {
            ContainerHolder.Container = Container;
        }
    }
}