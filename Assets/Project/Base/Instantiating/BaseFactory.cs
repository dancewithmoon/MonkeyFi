using System.Collections.Generic;
using System.Threading.Tasks;
using Base.AssetManagement;
using Base.States;
using UnityEngine;

namespace Base.Instantiating
{
    public abstract class BaseFactory : IPreloadedInLoadLevel, ICleanUp
    {
        protected readonly IAssets assets;
        private readonly IInstantiateService _instantiateService;

        private readonly List<GameObject> _instantiated = new List<GameObject>();
        
        protected BaseFactory(IAssets assets, IInstantiateService instantiateService)
        {
            this.assets = assets;
            _instantiateService = instantiateService;
        }

        public abstract Task Preload();
        
        public virtual void CleanUp()
        {
            foreach (GameObject obj in _instantiated)
            {
                if(obj != null)
                    Destroy(obj);
            }
            
            _instantiated.Clear();
        }

        protected GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject instance = _instantiateService.Instantiate(prefab);
            Register(instance);
            return instance;
        }
        
        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, at);
            Register(instance);
            return instance;
        }

        protected GameObject InstantiateRegistered(GameObject prefab, Transform parent)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, parent);
            Register(instance);
            return instance;
        }
        
        protected GameObject InstantiateRegistered(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            GameObject instance = _instantiateService.Instantiate(prefab, at, rotation);
            Register(instance);
            return instance;
        }
        
        protected T InstantiateRegistered<T>(T prefab, Transform parent) where T : MonoBehaviour
        {
            T instance = _instantiateService.Instantiate(prefab, parent);
            Register(instance.gameObject);
            return instance;
        }

        protected async Task<GameObject> InstantiateRegistered(string path)
        {
            GameObject asset = await assets.Load<GameObject>(path);
            GameObject instance = _instantiateService.Instantiate(asset);
            Register(instance);
            return instance;
        }
        
        protected async Task<GameObject> InstantiateRegistered(string path, Transform parent)
        {
            GameObject asset = await assets.Load<GameObject>(path);
            GameObject instance = _instantiateService.Instantiate(asset, parent);
            Register(instance);
            return instance;
        }

        protected async Task<GameObject> InstantiateRegistered(string path, Vector3 at, Transform parent)
        {
            GameObject asset = await assets.Load<GameObject>(path);
            GameObject instance = _instantiateService.Instantiate(asset, at, parent);
            Register(instance);
            return instance;
        }
        
        protected async Task<GameObject> InstantiateRegistered(string path, Vector3 at)
        {
            GameObject asset = await assets.Load<GameObject>(path);
            GameObject instance = _instantiateService.Instantiate(asset, at);
            Register(instance);
            return instance;
        }

        private void Register(GameObject gameObject) => 
            _instantiated.Add(gameObject);
        
        private static void Destroy(GameObject obj) => Object.Destroy(obj);
    }
}