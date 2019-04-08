using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileCommon>> GetFiles(bool withPublic); 
        Task<FileCommon> DownloadFile(FileCommon file);
        Task<int> UploadFile(int userId, FileCommon file);
        Task<bool> DeleteFile(int userId, FileCommon file);
    }
}
