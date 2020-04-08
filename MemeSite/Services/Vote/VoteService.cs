using AutoMapper;
using MemeSite.Model;
using MemeSite.Repository;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class VoteService : GenericService<Vote>, IVoteService
    {

        public VoteService(IGenericRepository<Vote> _voteRepository) : base(_voteRepository)
        {
            
        }

        public async Task<bool> InsertVote(SendVoteVM voteVM, string userId)
        {
            if (await _repository.IsExistAsync(m => m.MemeRefId == voteVM.MemeRefId && m.UserId == userId) == false)
            {
                Vote vote = new Vote()
                {
                    Value = voteVM.Value,
                    MemeRefId = voteVM.MemeRefId,
                    UserId = userId,
                };
                await _repository.InsertAsync(vote);
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateVote(SendVoteVM voteVM, string userId)
        {
            var vote = await _repository.FindAsync(m => m.MemeRefId == voteVM.MemeRefId && m.UserId == userId);
            if(vote == null || voteVM.Value == vote.Value)
            {
                return false;
            }
            vote.Value = voteVM.Value;
            await _repository.UpdateAsync(vote);
            return true;
        }

        public async Task<int> CountMemeValue(int memeId, int value) => 
            await _repository.CountAsync(m => m.MemeRefId == memeId && m.Value == value);

        public async Task<int> GetMemeRate(int memeId) =>
            await CountMemeValue(memeId, 1) - await CountMemeValue(memeId, -1);

        public async Task<int?> GetValueIfExist(int memeId, string userId)
        {
            var vote = await _repository.FindAsync(m => m.MemeRefId == memeId && m.UserId == userId);
            if(vote == null)
            {
                return null;
            }
            return vote.Value;
        }

    }
}
