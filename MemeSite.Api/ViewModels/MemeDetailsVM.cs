using MemeSite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.ViewModels
{
    public class MemeDetailsVM
    {
        public int MemeId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }
        public string Title { get; set; }
        public string Txt { get; set; }
        public string ByteHead { get; set; }
        public byte[] ByteImg { get; set; }
        public CategoryVM Category { get; set; }
        public int Rate { get; set; }
        public int CommentCount { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsArchived { get; set; }
        public bool IsVoted { get; set; }
        public Value? VoteValue { get; set; }
        public bool IsFavourite { get; set; }
    }
}
