using System.Threading.Tasks;
using UnityEngine;

namespace MonkeyBusiness.Infrastructure.Factory
{
    public interface IMonkeyBusinessFactory
    {
        Task<GameObject> CreateHero(Vector3 at);
    }
}