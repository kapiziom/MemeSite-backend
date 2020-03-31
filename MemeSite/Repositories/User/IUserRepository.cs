using MemeSite.Model;
using MemeSite.ViewModels;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public interface IUserRepository
    {
        string GetUsernameById(string userId);
    }
}
