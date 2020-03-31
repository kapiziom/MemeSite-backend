using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemeSite.Data;
using MemeSite.Model;
using MemeSite.ViewModels;

namespace MemeSite.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VoteRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        public bool SendVote(SendVoteVM voteVM, string userId)
        {
            bool check = DidUserVote(voteVM, userId);
            if(check == true)
            {
                var vote = _applicationDbContext.Votes.FirstOrDefault(m => m.MemeRefId == voteVM.MemeRefId && m.UserId == userId);
                if (voteVM.Value == vote.Value)
                {
                    return false;
                }
                vote.Value = voteVM.Value;
            }
            else
            {
                var vote = new Vote()
                {
                    Value = voteVM.Value,
                    MemeRefId = voteVM.MemeRefId,
                    UserId = userId,
                };
                _applicationDbContext.Votes.Add(vote);
            }
            _applicationDbContext.SaveChanges();
            return true;
        }

        public bool DidUserVote(SendVoteVM vote, string userId)
        {
            var check = _applicationDbContext.Votes.FirstOrDefault(m => m.MemeRefId == vote.MemeRefId && m.UserId == userId);
            if (check != null)
            {
                return true;
            }
            else return false;
        }
    }
}
