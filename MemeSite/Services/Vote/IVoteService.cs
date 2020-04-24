using MemeSite.Data.Models;
using MemeSite.Data.Models.Common;
using MemeSite.Data.Models.Enums;
using MemeSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Services
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
