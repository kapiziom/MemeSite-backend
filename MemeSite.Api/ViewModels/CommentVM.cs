using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.ViewModels
{
    public class CommentVM
    {
        public int CommentId { get; set; }
        public int MemeId { get; set; }
        public string Txt { get; set; }
        public string CreationDate { get; set; }
        public string EditDate { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}
