using MemeSite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.ViewModels
{
    public class SendVoteVM
    {
        [Required]
        public Value Value { get; set; }//-1 or 1

        [Required]
        public int MemeRefId { get; set; }
    }

    
}
