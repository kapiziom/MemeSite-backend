using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.ViewModels
{
    public class FileUpload
    {
        public IFormFile File { get; set; }
        public string Title { get; set; }
    }
}
