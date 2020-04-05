using AutoMapper;
using MemeSite.Model;
using MemeSite.Repositories;
using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<bool> InsertVote(SendVoteVM voteVM, string userId)
        {
            if (await _voteRepository.GetIfUserVoted(voteVM.MemeRefId, userId) == null)
            {
                Vote vote = new Vote()
                {
                    Value = voteVM.Value,
                    MemeRefId = voteVM.MemeRefId,
                    UserId = userId,
                };
                await _voteRepository.InsertVote(vote);
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateVote(SendVoteVM voteVM, string userId)
        {
            var vote = await _voteRepository.GetIfUserVoted(voteVM.MemeRefId, userId);
            if(vote == null || voteVM.Value == vote.Value)
            {
                return false;
            }
            vote.Value = voteVM.Value;
            await _voteRepository.UpdateVote(vote);
            return true;
        }

        public async Task<int> CountMemeValue(int memeId, int value)
        {
            return await _voteRepository.Count(memeId, value);
        }

        public async Task<int> GetMemeRate(int memeId)
        {
            return await CountMemeValue(memeId, 1) - await CountMemeValue(memeId, -1);
        }
    }
}
