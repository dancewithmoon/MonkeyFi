using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Base.AssetManagement
{
    public interface IAssets
    {
        Task<T> Load<T>(string path) where T : Object;
        Task<IEnumerable<T>> LoadAll<T>(string path) where T : Object;
        void CleanUp();
    }
}