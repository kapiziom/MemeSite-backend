using AutoMapper;
using FluentValidation;
using MemeSite.Application.Interfaces;
using MemeSite.Application.ViewModels;
using MemeSite.Domain.Interfaces;
using MemeSite.Domain.Models;
using System.Net;
using System.Threading.Tasks;

namespace MemeSite.Application.Services
{
    public class VoteService : GenericService<Vote>, IVoteService
    {

        public VoteService(
            IVoteRepository _voteRepository,
            IValidator<Vote> validator) : base(_voteRepository, validator) { }


        public async Task<Result<Vote>> InsertVote(SendVoteVM voteVM, string userId)
        {
            Vote vote = new Vote()
            {
                Value = voteVM.Value,
                MemeRefId = voteVM.MemeRefId,
                UserId = userId,
            };
            if (await _repository.IsExistAsync(m => m.MemeRefId == voteVM.MemeRefId && m.UserId == userId) == false)
            {
                var result = await ValidateAsync(vote);
                if (result.Succeeded)
                {
                    await _repository.InsertAsync(vote);
                    return result;
                }
                throw new MemeSiteException(HttpStatusCode.BadRequest, null, result);
            }
            else throw new MemeSiteException(HttpStatusCode.Conflict, "U have voted for this");
        }

        public async Task<Result<Vote>> UpdateVote(SendVoteVM voteVM, string userId)
        {
            var vote = await _repository.FindAsync(m => m.MemeRefId == voteVM.MemeRefId && m.UserId == userId);
            if (vote == null) throw new MemeSiteException(HttpStatusCode.NotFound, "Not Found");
            if (voteVM.Value == vote.Value)
            {
                throw new MemeSiteException(HttpStatusCode.Conflict, "Value is the same");
            }
            vote.Value = voteVM.Value;
            var result = await ValidateAsync(vote);
            if (result.Succeeded)
            {
                result.Value = await _repository.UpdateAsync(vote);
                return result;
            }
            throw new MemeSiteException(HttpStatusCode.BadRequest, null, result);
        }

        public async Task<int> CountMemeValue(int memeId, Value value) => 
            await _repository.CountAsync(m => m.MemeRefId == memeId && m.Value == value);

        public async Task<int> GetMemeRate(int memeId) =>
            await CountMemeValue(memeId, Value.upvote) - await CountMemeValue(memeId, Value.downvote);

        public async Task<Value?> GetValueIfExist(int memeId, string userId)
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
