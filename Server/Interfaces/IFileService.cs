using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileCommon>> GetFiles(int userId, bool withPublic); 
        Task<FileCommon> DownloadFile(FileCommon file);
        Task<int> UploadFile(FileCommon file);
        Task<bool> DeleteFile(FileCommon file);
        Task<FileCommon> UpadateFileMetadata(FileCommon file);
    }
}
