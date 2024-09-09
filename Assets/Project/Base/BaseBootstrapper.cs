using Base.AssetManagement;
using Base.Instantiating;
using Base.Services;
using Base.Services.CoroutineRunner;
using UnityEngine;
using Zenject;

namespace Base
{
    [CreateAssetMenu(fileName = "Base Bootstrapper", menuName = "Installers/BaseBootstrapper")]
    public class BaseBootstrapper : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ContainerHolder>().AsSingle();
            
            Container.Bind<ICoroutineRunner>().FromMethod(GetCoroutineRunner).AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            
            Container.Bind<IInstantiateService>().To<ZenjectInstantiateService>().AsSingle();
            Container.Bind<IAssets>().To<ResourcesAssets>().AsSingle();
        }
        
        private static ICoroutineRunner GetCoroutineRunner()
        {
            CoroutineRunner coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(coroutineRunner);
            return coroutineRunner;
        }
    }
}
