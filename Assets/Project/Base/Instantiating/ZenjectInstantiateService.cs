using Base.Services;
using UnityEngine;

namespace Base.Instantiating
{
    public class ZenjectInstantiateService : IInstantiateService
    {
        private readonly ContainerHolder _container;

        public ZenjectInstantiateService(ContainerHolder container)
        {
            _container = container;
        }
        
        public GameObject Instantiate(GameObject prefab) => 
            _container.Container.InstantiatePrefab(prefab);

        public GameObject Instantiate(GameObject prefab, Vector3 at) =>
            Instantiate(prefab, at, null);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent) => 
            _container.Container.InstantiatePrefab(prefab, at, Quaternion.identity, parent);

        public GameObject Instantiate(GameObject prefab, Transform parent) => 
            _container.Container.InstantiatePrefab(prefab, parent);

        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation) => 
            _container.Container.InstantiatePrefab(prefab, at, rotation, null);

        public T Instantiate<T>(T prefab) where T : Object
        {
            T obj = Object.Instantiate(prefab);
            _container.Container.Inject(obj);
            return obj;
        }

        public T Instantiate<T>(T prefab, Transform parent) where T : Object
        {
            T obj = Object.Instantiate(prefab, parent);
            _container.Container.Inject(obj);
            return obj;
        }
    }
}