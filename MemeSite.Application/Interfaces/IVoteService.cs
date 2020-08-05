using MemeSite.Application.ViewModels;
using MemeSite.Domain.Models;
using System.Threading.Tasks;

namespace MemeSite.Application.Interfaces
{
    public interface IVoteService : IGenericService<Vote>
    {
        Task<Result<Vote>> InsertVote(SendVoteVM vote, string userId);
        Task<Result<Vote>> UpdateVote(SendVoteVM vote, string userId);
        Task<int> CountMemeValue(int memeId, Value value);
        Task<int> GetMemeRate(int memeId);
        Task<Value?> GetValueIfExist(int memeId, string userId);
    }
}
