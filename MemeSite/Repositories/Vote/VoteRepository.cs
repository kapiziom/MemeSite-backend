using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemeSite.Data;
using MemeSite.Model;
using MemeSite.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MemeSite.Repositories
{
    public class VoteRepository : GenericRepository<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext _applicationDbContext) : base(_applicationDbContext) { }


        public bool SendVote(SendVoteVM voteVM, string userId)
        {
            bool check = DidUserVote(voteVM.MemeRefId, userId);
            if(check == true)
            {
                return false;
            }
            if (_applicationDbContext.Memes.FirstOrDefault(m => m.MemeId == voteVM.MemeRefId) == null)
            {
                return false;
            }
            var vote = new Vote()
            {
                Value = voteVM.Value,
                MemeRefId = voteVM.MemeRefId,
                UserId = userId,
            };
            _applicationDbContext.Votes.Add(vote);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public bool ChangeVote(SendVoteVM voteVM, string userId)
        {
            var vote = _applicationDbContext.Votes.FirstOrDefault(m => m.MemeRefId == voteVM.MemeRefId && m.UserId == userId);
            if (voteVM.Value == vote.Value)
            {
                return false;
            }
            vote.Value = voteVM.Value;
            _applicationDbContext.SaveChanges();
            return true;
        }

        public bool DidUserVote(int memeId, string userId)
        {
            var check = _applicationDbContext.Votes.FirstOrDefault(m => m.MemeRefId == memeId && m.UserId == userId);
            if (check != null)
            {
                return true;
            }
            else return false;
        }

        public int GetMemeRate(int id)
        {
            var plus = _applicationDbContext.Votes.Where(m => m.Value == 1 && m.MemeRefId == id).Count();
            var minus = _applicationDbContext.Votes.Where(m => m.Value == -1 && m.MemeRefId == id).Count();
            return plus - minus;
        }

        public async Task<Vote> GetById(int id) =>
            await _applicationDbContext.Votes
            .FirstOrDefaultAsync(m => m.VoteId == id);

        public async Task<Vote> GetIfUserVoted(int memeId, string userId) =>
            await _applicationDbContext.Votes
            .FirstOrDefaultAsync(m => m.MemeRefId == memeId && m.UserId == userId);

        public async Task InsertVote(Vote vote)
        {
            await _applicationDbContext.Votes.AddAsync(vote);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateVote(Vote vote)
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> DidUserVote1(int memeId, string userId)
        {
            var check = await _applicationDbContext.Votes.FirstOrDefaultAsync(m => m.MemeRefId == memeId && m.UserId == userId);
            if (check != null)
            {
                return true;
            }
            else return false;
        }

        public async Task<int> Count(int memeId, int value) => 
            await _applicationDbContext.Votes
            .Where(m => m.MemeRefId == memeId && m.Value == value)
            .CountAsync();
    }
}
