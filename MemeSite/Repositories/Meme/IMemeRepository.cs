﻿using MemeSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemeSite.Repositories
{
    public interface IMemeRepository
    {
        string UploadImage(FileUpload file);
        void UploadMeme(MemeUploadVM m, string userId);
    }
}
