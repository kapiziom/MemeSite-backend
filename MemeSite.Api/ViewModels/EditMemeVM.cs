using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Api.ViewModels
{
    public class EditMemeVM
    {
        public int MemeId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Txt { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
