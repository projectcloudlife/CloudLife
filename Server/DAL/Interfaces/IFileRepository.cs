using Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileDB>> GetWhere(Func<FileDB, bool> expr);
        Task<FileDB> DownloadFile(int fileId);
        Task<int> UploadFile(FileDB file);
        Task<bool> DeleteFile(int fileId);
    }
}
