using System.Threading.Tasks;

namespace Services.UserProgress
{
    public interface IUserProgressService
    {
        Task LoadProgress();
        void SaveProgress();
    }
}