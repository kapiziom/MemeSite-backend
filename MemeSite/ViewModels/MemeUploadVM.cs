using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class MemeUploadVM
    {
        [Required]
        public string Title { get; set; }
        public string Txt { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        [Required]
        public string ByteHead { get; set; }
        [Required]
        public byte[] FileByte { get; set; }
    }

}
