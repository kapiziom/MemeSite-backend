using MemeSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemeSite.Application.ViewModels
{
    public class SendVoteVM
    {
        [Required]
        public Value Value { get; set; }//-1 or 1

        [Required]
        public int MemeRefId { get; set; }
    }

    
}
