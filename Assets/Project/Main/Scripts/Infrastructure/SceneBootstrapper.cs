using Base.Services;
using Zenject;

namespace Infrastructure
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