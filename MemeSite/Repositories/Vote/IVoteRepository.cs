using MemeSite.Model;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemeSite.Repositories
{
    public interface IVoteRepository : IGenericRepository<Vote>
    {
        bool SendVote(SendVoteVM vote, string userId);
        bool ChangeVote(SendVoteVM vote, string userId);
        bool DidUserVote(int memeId, string userId);
        int GetMemeRate(int id);//get rate of meme  {id}

        Task<Vote> GetById(int id);
        Task<Vote> GetIfUserVoted(int memeId, string userId);
        Task InsertVote(Vote vote);
        Task UpdateVote(Vote vote);
        Task<bool> DidUserVote1(int memeId, string userId);
        Task<int> Count(int memeId, int value);
    }
}
