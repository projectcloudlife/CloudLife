using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface ICloudFileService
    {
        Task<IEnumerable<FileCommon>> GetFiles(bool withPublic);
        Task<bool> UploadFile(FileCommon fileCommon);
        Task<FileCommon> DownloadFile(FileCommon file);
        Task<bool> DeleteFile(FileCommon file);
    }
}
