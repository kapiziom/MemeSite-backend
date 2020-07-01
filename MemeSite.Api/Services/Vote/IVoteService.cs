using MemeSite.Domain;
using MemeSite.Domain.Common;
using MemeSite.Domain.Enums;
using MemeSite.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.Services
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
