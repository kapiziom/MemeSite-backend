using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Repositories
{
    public interface IVoteRepository
    {
        bool SendVote(SendVoteVM vote, string userId);
        bool DidUserVote(SendVoteVM vote, string userId);
    }
}
