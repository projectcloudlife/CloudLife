﻿using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
    public interface IFileRepository:IDisposable
    {
        Task<IEnumerable<FileDB>> GetWhere(Func<FileDB, bool> expr);
        Task<FileDB> DownloadFile(int fileId);

        // returns file id.
        Task<int> UploadFile(FileDB file);
        Task<bool> DeleteFile(int fileId);

        Task<FileDB> UpdateFile(FileDB file);
    }
}
