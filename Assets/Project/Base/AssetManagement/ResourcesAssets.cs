using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.AssetManagement
{
    public class ResourcesAssets : IAssets
    {
        private readonly Dictionary<string, Object> _completedCache = new();

        private readonly Dictionary<string, IEnumerable<Object>> _completedArraysCache = new();

        public Task<T> Load<T>(string path) where T : Object
        {
            if (_completedCache.TryGetValue(path, out Object cachedObject))
                return Task.FromResult(cachedObject as T);

            T asset = Resources.Load<T>(path);
            _completedCache.Add(path, asset);
            return Task.FromResult(asset);
        }

        public Task<IEnumerable<T>> LoadAll<T>(string path) where T : Object
        {
            if (_completedArraysCache.TryGetValue(path, out IEnumerable<Object> cachedAssets))
                return Task.FromResult(cachedAssets.Cast<T>());
            
            T[] assets = Resources.LoadAll<T>(path);
            _completedArraysCache.Add(path, assets);
            return Task.FromResult((IEnumerable<T>)assets);
        }

        public void CleanUp()
        {
            foreach (Object asset in _completedCache.Values)
            {
                Resources.UnloadAsset(asset);
            }
            _completedCache.Clear();
        }
    }
}