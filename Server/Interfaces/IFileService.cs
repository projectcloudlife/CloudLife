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
        Task<int> UploadFile(FileCommon file);
        Task<bool> DeleteFile(FileCommon file);
    }
}
