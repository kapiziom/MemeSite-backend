using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public interface IVoteService : IGenericService<Vote>
    {
        Task<bool> InsertVote(SendVoteVM vote, string userId);
        Task<bool> UpdateVote(SendVoteVM vote, string userId);
        Task<int> CountMemeValue(int memeId, int value);
        Task<int> GetMemeRate(int memeId);
        Task<int?> GetValueIfExist(int memeId, string userId);
    }
}
