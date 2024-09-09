using UnityEngine;

namespace Base.Instantiating
{
    public interface IInstantiateService
    {
        GameObject Instantiate(GameObject prefab);
        GameObject Instantiate(GameObject prefab, Vector3 at);
        GameObject Instantiate(GameObject prefab, Vector3 at, Transform parent);
        GameObject Instantiate(GameObject prefab, Transform parent);
        GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation);
        T Instantiate<T>(T prefab) where T : Object;
        T Instantiate<T>(T prefab, Transform parent) where T : Object;
    }
}